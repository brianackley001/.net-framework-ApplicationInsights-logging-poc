using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace LoadTest_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly TelemetryConfiguration _telemetryConfiguration;
        private readonly Random _random;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TelemetryClient telemetryClient, TelemetryConfiguration telemetryConfiguration)
        {
            this._logger = logger;
            this._telemetryClient = telemetryClient;

            // In a real app, you wouldn't need the TelemetryConfiguration here.
            // This is included in this sample because it allows you to debug and verify that the configuration at runtime matches the expected configuration.
            this._telemetryConfiguration = telemetryConfiguration;
            _random = new Random();
        }

         [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Bok"
            })
            .ToArray();
        }


        //[HttpPost("event")]
        //public async Task<IActionResult> PostEvent([FromBody] string eventText)
        //{
        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            _telemetryClient.TrackEvent(eventText);
        //        });

        //        return Ok(new ApiResponse
        //        {
        //            Id = _random.Next(13, 50000000).ToString(),
        //            Success = true,
        //            Message = $"PostEvent: {eventText}"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log Exception
        //        _logger.Log(LogLevel.Error, ex, ex?.Message);
        //        return StatusCode(500);
        //    }
        //}
    }
}
