using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;

namespace LoadTest_API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AppInsightsController : ControllerBase
    {
        private readonly ILogger<AppInsightsController> _logger;
        private readonly TelemetryClient _telemetryClient;
        private readonly TelemetryConfiguration _telemetryConfiguration;
        private readonly Random _random;

        public AppInsightsController(ILogger<AppInsightsController> logger, TelemetryClient telemetryClient, TelemetryConfiguration telemetryConfiguration)
        {
            this._logger = logger;
            this._telemetryClient = telemetryClient;

            // In a real app, you wouldn't need the TelemetryConfiguration here.
            // This is included in this sample because it allows you to debug and verify that the configuration at runtime matches the expected configuration.
            this._telemetryConfiguration = telemetryConfiguration;
            _random = new Random();
        }



        [HttpPost("event")]
        public async Task<IActionResult> PostEvent([FromBody] PostPayload payload)
        {
            try
            {
                await Task.Run(() =>
                {
                    _telemetryClient.TrackEvent(payload.Name, payload.Properties);
                });

                return Ok(new ApiResponse
                {
                    Id = _random.Next(13, 50000000).ToString(),
                    Success = true,
                    Message = $"PostEvent: {payload.Name}"
                });
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.Log(LogLevel.Error, ex, ex?.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("trace")]
        public async Task<IActionResult> PostTrace([FromBody] PostPayload payload)
        {
            try
            {
                await Task.Run(() =>
                {
                    _telemetryClient.TrackTrace(payload.Name, payload.Properties);
                });

                return Ok(new ApiResponse
                {
                    Id = _random.Next(13, 50000000).ToString(),
                    Success = true,
                    Message = $"PostTrace: {payload.Name}"
                });
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.Log(LogLevel.Error, ex, ex?.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("pageView")]
        public async Task<IActionResult> PostPageView([FromBody] string pageViewText)
        {
            try
            {
                await Task.Run(() =>
                {
                    _telemetryClient.TrackPageView(pageViewText);
                });

                return Ok(new ApiResponse
                {
                    Id = _random.Next(13, 50000000).ToString(),
                    Success = true,
                    Message = $"PostPageView: {pageViewText}"
                });
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.Log(LogLevel.Error, ex, ex?.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("request")]
        public async Task<IActionResult> PostRequest([FromBody] string pageNameText)
        {
            try
            {
                await Task.Run(() =>
                {
                    var ticks = _random.Next(1, 302);
                    var dateTime = new DateTimeOffset(DateTime.Now);
                    var duration = new TimeSpan(ticks);
                    _telemetryClient.TrackRequest(pageNameText, dateTime, duration, "OK", true);
                });

                return Ok(new ApiResponse
                {
                    Id = _random.Next(13, 50000000).ToString(),
                    Success = true,
                    Message = $"PostPageView: {pageNameText}"
                });
            }
            catch (Exception ex)
            {
                // Log Exception
                _logger.Log(LogLevel.Error, ex, ex?.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("exception")]
        public async Task<IActionResult> PostException([FromBody] ExceptionObject ex)
        {
            try
            {
                await Task.Run(() =>
                {
                    _telemetryClient.TrackException(ex.Exception, ex.Properties);
                });

                return Ok(new ApiResponse
                {
                    Id = _random.Next(13, 50000000).ToString(),
                    Success = true,
                    Message = $"PostException: {ex.Exception?.Message}"
                });
            }
            catch (Exception e)
            {
                // Log Exception
                _logger.Log(LogLevel.Error, e, e?.Message);
                return StatusCode(500);
            }
        }
    }
}
