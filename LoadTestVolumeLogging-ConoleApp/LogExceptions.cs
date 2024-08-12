using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    internal class LogExceptions
    {
        private readonly HttpClient _client = new HttpClient();
        private Random _random;
        private Dictionary<int, string> _exceptionNames = new Dictionary<int, string>();
        private Dictionary<int, string> _userIds = new Dictionary<int, string>();

        public async Task DoWork(int count)
        {
            InitExceptionReasons(); 
            InitUserIds();

            _random = new Random();
            _client.BaseAddress = new Uri("https://localhost:7203/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            

            for (int i = 0; i < count; i++)
            {
                try
                {
                    var sleepValue = _random.Next(250, 400);
                    await Task.Delay(sleepValue);

                    var exObj = GetExceptionItem(i);
                    await CallApi(exObj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LogException loop exception, iteration {i}: {ex.Message}");
                }
            }
        }


        private async Task<ApiResponse> CallApi(ExceptionObject exObj)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/exception", exObj);
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();

            return requestResponse;
        }

        private ExceptionObject GetExceptionItem(int loopCounter)
        {
            ExceptionObject exObj = new ExceptionObject();

            var randomIndex = _random.Next(1, 36);
            var userId = _userIds.ElementAt(randomIndex).Value;

            if (loopCounter % 7 == 0)
            {
                exObj.Exception = new NullReferenceException($"Null Reference Load Test Exception, loop iteration {loopCounter}");
                exObj.Properties = new Dictionary<string, string>
                {
                    {"ID", loopCounter.ToString() },
                    {"Name", loopCounter.ToString() },
                    {"LoadTest", "true" },
                    {"UserId", $"{userId}" },
                    {"ExceptionParameter1", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameter2", $"Value-{_random.Next(100, 900000)}" },
                    {"ExceptionParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
            }
            if(loopCounter % 9== 0 && loopCounter % 7 != 0)
            {
                exObj.Exception = new ArgumentOutOfRangeException($"Argument Out Of Range Exception Load Test Exception, loop iteration {loopCounter}");
                exObj.Properties = new Dictionary<string, string>
                {
                    {"ID", loopCounter.ToString() },
                    {"UserId", $"{userId}" },
                    {"Arg1", _random.Next(234, 12999999).ToString() },
                    {"Arg2", $"Test-String-{_random.Next(1, 500)}"},
                    {"Arg3", $"ProfileExpectedValue-{_random.Next(10000, 500000)}"},
                    {"LoadTest", "true" }
                };
            }
            if (loopCounter % 9 != 0 && loopCounter % 7 != 0)
            {
                randomIndex = _random.Next(1, 12);
                var bizExceptionName = _exceptionNames.ElementAt(randomIndex).Value;
                exObj.Exception = new ApplicationException($"{bizExceptionName}, loop iteration {loopCounter}");
                exObj.Properties = new Dictionary<string, string>
                {
                    {"ID", loopCounter.ToString() },
                    {"UserId", $"{userId}" },
                    {"Parameter1", _random.Next(234, 12999999).ToString() },
                    {"Parameter2", $"Vendor-String-{_random.Next(1, 500)}"},
                    {"Parameter3", $"VendorExpectedValue-{_random.Next(10000, 500000)}"},
                    {"Parameter4", $"BusinessRule-{_random.Next(1, 500)}"},
                    {"LoadTest", "true" }
                };
            }

            return exObj;
        }


        private void InitUserIds()
        {
            for (int i = 1; i < 37; i++)
            {
                _userIds.Add(i, Guid.NewGuid().ToString());
            }
        }
        private void InitExceptionReasons()
        {
            _exceptionNames.Add(1, "Invalid Business Rule X");
            _exceptionNames.Add(2, "Invalid Business Rule Y");
            _exceptionNames.Add(3, "Invalid Business Rule Z");
            _exceptionNames.Add(4, "Invalid Business Rule A");
            _exceptionNames.Add(5, "Invalid Business Rule XYZ");
            _exceptionNames.Add(6, "Invalid Business Rule 9999876");
            _exceptionNames.Add(7, "Invalid Business Rule P9ui876Gtr");
            _exceptionNames.Add(8, "Profile Business Log Rule Violated");
            _exceptionNames.Add(9, "Insufficient Security Permissions");
            _exceptionNames.Add(10, "Invalid Business Rule 9123456");
            _exceptionNames.Add(11, "Invalid Business Rule AaaaaDdddd");
            _exceptionNames.Add(12, "Invalid Business Rule 9a8s7d6f6gA");
        }
    }
}
