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
            Thread.Sleep(10000);
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

            var loopCounter = 7500;
            var tasks = new List<Task>();
            tasks.Add(logEvent.DoWork(loopCounter));
            tasks.Add(logRequest.DoWork(loopCounter));
            tasks.Add(logTrace.DoWork(loopCounter));
            tasks.Add(logException.DoWork(loopCounter));
            tasks.Add(logPageView.DoWork(loopCounter));

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { Console.WriteLine($"Task.WhenAll has completed (loopCounter = {loopCounter})"); }
        }

        //static async Task RunAsync()
        //{
        //    // Update port # in the following line.
        //    client.BaseAddress = new Uri("https://localhost:7203/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {
        //        // Create a new product
        //        //Product product = new Product
        //        //{
        //        //    Name = "Gizmo",
        //        //    Price = 100,
        //        //    Category = "Widgets"
        //        //};

        //        //var url = await CreateProductAsync(product);
        //        //Console.WriteLine($"Created at {url}");

        //        // Get the Test Output
        //        var test = await GetTestAsync("Test");
        //        Console.WriteLine(test.Success);

        //        //// Update the product
        //        //Console.WriteLine("Updating price...");
        //        //product.Price = 80;
        //        //await UpdateProductAsync(product);

        //        //// Get the updated product
        //        //product = await GetProductAsync(url.PathAndQuery);
        //        //ShowProduct(product);

        //        //// Delete the product
        //        //var statusCode = await DeleteProductAsync(product.Id);
        //        //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    Console.ReadLine();
        //}
    }
}