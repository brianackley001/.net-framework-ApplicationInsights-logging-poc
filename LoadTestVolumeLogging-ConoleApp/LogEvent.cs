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


        public async Task DoWork(int count)
        {
            InitEventNames();
            _random = new Random();
            _client.BaseAddress = new Uri("https://localhost:7203/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < count; i++)
            {
                var randomIndex = _random.Next(1, 16);
                var eventName = _eventNames.ElementAt(randomIndex).Value;
                var sleepValue = _random.Next(750, 5000);
                await Task.Delay(sleepValue);
                await CallApi(eventName);
            }
        }
         
        private async Task<ApiResponse> CallApi(string eventName)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/event", eventName); 
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();
            
            return requestResponse;
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
