using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();

        public class ApiResponse
        {
            public string Id { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("console app has started");
            Thread.Sleep(15000);
            Console.WriteLine("Begin Processing...");
            //RunAsync().GetAwaiter().GetResult();


            ExcuteTaskCollection().GetAwaiter().GetResult();
            Console.Read();
        }
            
        static async Task<ApiResponse> GetTestAsync(string path)
        {
            ApiResponse requestResponse = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                requestResponse = await response.Content.ReadAsAsync<ApiResponse>();
            }
            return requestResponse;
        }
        static async Task ExcuteTaskCollection()
        {
            var logEvent = new LogEvent();
            var logRequest = new LogRequest();
            var logTrace = new LogTrace();
            var logException = new LogExceptions();
            var logPageView = new LogPageView();

            var loopCounter = 1000000;
            var batchSize = 5000;
            var itemsProcessed = 0;

            while (itemsProcessed < loopCounter)
            {
                var tasks = new List<Task>();
                tasks.Add(logEvent.DoWork(batchSize));
                tasks.Add(logRequest.DoWork(batchSize));
                tasks.Add(logTrace.DoWork(batchSize));
                tasks.Add(logException.DoWork(batchSize));

                try
                {
                        Console.WriteLine($"... begin processing batch of {batchSize}. itemsProcessedToDate: {itemsProcessed}, target: {loopCounter}");
                        await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"PROGRAM: while (itemsProcessed < loopCounter). batch of {batchSize}. itemsProcessedToDate: {itemsProcessed}, target: {loopCounter}");
                    Console.WriteLine($"!!!  Exception: {ex.Message}");
                }
                finally
                {
                    itemsProcessed += batchSize;
                    Console.WriteLine($"Task.WhenAll has completed batch of {batchSize}. itemsProcessedToDate: {itemsProcessed}, target: {loopCounter}");
                    Thread.Sleep(1000);
                    Console.WriteLine($"Thread.Sleep(1000) has completed.");
                }
            }
        }

    }
}