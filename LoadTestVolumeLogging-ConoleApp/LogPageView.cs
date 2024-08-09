using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    internal class LogPageView
    {
        private readonly HttpClient _client = new HttpClient();
        private Random _random;
        private Dictionary<int, string> _pageNames = new Dictionary<int, string>();

        public async Task DoWork(int count)
        {
            InitPageNames();

            _random = new Random();
            _client.BaseAddress = new Uri("https://localhost:7203/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < count; i++)
            {
                var sleepValue = _random.Next(1000, 9000);
                var randomIndex = _random.Next(1, 9);
                var pageName = _pageNames.ElementAt(randomIndex).Value;
                await Task.Delay(sleepValue);

                await CallApi(pageName);
            }
        }


        private async Task<ApiResponse> CallApi(string pageName)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/request", pageName);
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();

            return requestResponse;
        }

        private void InitPageNames()
        {
            _pageNames.Add(1, "Login");
            _pageNames.Add(2, "Logout");
            _pageNames.Add(3, "Contracts");
            _pageNames.Add(4, "Contractors");
            _pageNames.Add(5, "MonitoringActivities");
            _pageNames.Add(6, "ContractDetail");
            _pageNames.Add(7, "ContractorDetail");
            _pageNames.Add(8, "Profile");
            _pageNames.Add(9, "Dashboard");
        }
    }
}
