using LoadTestVolumeLogging_ConoleApp;
using LoadTestVolumeLogging_ConsoleApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    internal class LogEvent
    {
        private readonly HttpClient _client = new HttpClient();
        private Random _random;
        private Dictionary<int, string> _eventNames = new Dictionary<int, string>();
        private Dictionary<int, string> _userIds = new Dictionary<int, string>();


        public async Task DoWork(int count)
        {
            InitEventNames();
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
                    var randomIndex = _random.Next(1, 16);
                    var eventName = _eventNames.ElementAt(randomIndex).Value;
                    randomIndex = _random.Next(1, 36);
                    var userId = _userIds.ElementAt(randomIndex).Value;
                    var sleepValue = _random.Next(100, 375);
                    var properties = new Dictionary<string, string>
                {
                    {"UserId", $"{userId}" },
                    {"EventParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"EventParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
                    await Task.Delay(sleepValue);
                    var payload = new PostPayload { Properties = properties, Name = eventName };
                    await CallApi(payload);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LogEvent loop exception, iteration {i}: {ex.Message}");
                }
            }
        }
         
        private async Task<ApiResponse> CallApi(PostPayload payload)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/event", payload); 
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();
            
            return requestResponse;
        }

        private void InitUserIds()
        {
            for (int i = 1; i < 37; i++){
                _userIds.Add(i, Guid.NewGuid().ToString());
            }
        }

        private void InitEventNames()
        {
            _eventNames.Add(1, "Login");
            _eventNames.Add(2, "Logout");
            _eventNames.Add(3, "Profile_ButtonClick");
            _eventNames.Add(4, "EditProfile_ButtonClick");
            _eventNames.Add(5, "SaveProfile_ButtonClick");
            _eventNames.Add(6, "SearchContracts_ButtonClick");
            _eventNames.Add(7, "SearchContractResults_GridItemClick");
            _eventNames.Add(8, "EditContract_ButtonClick");
            _eventNames.Add(9, "SaveContract_ButtonClick");
            _eventNames.Add(10, "SearchContractor_ButtonClick");
            _eventNames.Add(11, "SearchContractorResults_GridItemClick");
            _eventNames.Add(12, "EditContractor_ButtonClick");
            _eventNames.Add(13, "SaveContractor_ButtonClick");
            _eventNames.Add(14, "AddMonitoringActivity_ButtonClick");
            _eventNames.Add(15, "EditMonitoringActivity_ButtonClick");
            _eventNames.Add(16, "SaveMonitoringActivity_ButtonClick");
        }
    }
}
