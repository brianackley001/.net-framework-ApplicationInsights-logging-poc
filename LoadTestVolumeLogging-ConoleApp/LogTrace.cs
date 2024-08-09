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

        public async Task DoWork(int count)
        {
            _random = new Random();
            _client.BaseAddress = new Uri("https://localhost:7203/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < count; i++)
            {
                var sleepValue = _random.Next(1000, 5000);
                var requestValue = _random.Next(1, 7500);
                await Task.Delay(sleepValue);

                await CallApi($"Trace-{requestValue}");
            }
        }


        private async Task<ApiResponse> CallApi(string traceName)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/trace", traceName);
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();

            return requestResponse;
        }
    }
}
