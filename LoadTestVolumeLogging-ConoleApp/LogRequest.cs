using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    internal class LogRequest
    {
        private readonly HttpClient _client = new HttpClient();
        private Random _random;

        public async Task DoWork(int count)
        {
            _random = new Random();
            _client.BaseAddress = new Uri("https://localhost:7203/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            for (int i = 0; i < count; i++)
            {
                var sleepValue = _random.Next(500, 1500);
                var requestValue = _random.Next(1, 3500);
                await Task.Delay(sleepValue);
                try
                {
                    await CallApi($"Request-{requestValue}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LogRequest loop exception, iteration {i}: {ex.Message}");
                }
            }
        }


        private async Task<ApiResponse> CallApi(string requestName)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync("AppInsights/request", requestName);
            response.EnsureSuccessStatusCode();
            // Deserialize the ApiResponse from the response body.
            requestResponse = await response.Content.ReadAsAsync<ApiResponse>();

            return requestResponse;
        }
    }
}
