using LoadTestVolumeLogging_ConoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    internal class LogTrace
    {
        private readonly HttpClient _client = new HttpClient();
        private Random _random;
        private Dictionary<int, string> _eventNames = new Dictionary<int, string>();
        private Dictionary<int, string> _userIds = new Dictionary<int, string>();

        public async Task DoWork(int count)
        {
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
                    var sleepValue = _random.Next(500, 1250);
                    var requestValue = _random.Next(1, 7500);
                    var randomIndex = _random.Next(1, 36);
                    var userId = _userIds.ElementAt(randomIndex).Value;
                    var properties = new Dictionary<string, string>
                {
                    {"UserId", $"{userId}" },
                    {"EventParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"EventParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
                    await Task.Delay(sleepValue);
                    var payload = new PostPayload
                    {
                        Name = $"Trace - {requestValue}",
                        Properties = properties
                    };
                    await CallApi(payload);
                }
                catch (Exception ex)
                { 
                    Console.WriteLine($"LogTrace loop exception, iteration {i}: {ex.Message}");
                }
            }
        }


        private async Task<ApiResponse> CallApi(PostPayload payload)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/trace", payload);
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();

            return requestResponse;
        }
        private void InitUserIds()
        {
            for (int i = 1; i < 37; i++)
            {
                _userIds.Add(i, Guid.NewGuid().ToString());
            }
        }
    }
}
