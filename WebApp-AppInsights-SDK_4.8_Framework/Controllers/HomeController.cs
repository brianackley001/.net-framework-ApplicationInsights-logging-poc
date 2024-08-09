using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp_AppInsights_SDK_4._8_Framework.Logging;


namespace WebApp_AppInsights_SDK_4._8_Framework.Controllers
{
    public class HomeController : Controller
    {
        private Random _random = new Random();
        private ILogger _logger;
        private readonly TelemetryClient telemetryClient;

        public HomeController(ILogger logger)
        {
            //_random = new Random();
            _logger = logger;
        }

        [HttpPost]
        public ActionResult TrackEvent()
        {
            string formValue = Request.Form["Text1"];
            Dictionary<string, string> properties = new Dictionary<string, string>
                {
                    {"EventParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"EventParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"EventParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
            _logger.TrackEvent(formValue, properties);
            return View("Index");
        }

        public ActionResult Index()
        {
            var traceProperties = new Dictionary<string, string>()
                {
                    { "Location", "HomeController" },
                    {"TraceParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"TraceParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"TracenParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"TraceParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
            _logger.TrackTrace($"SDK TRACE - Home.Index", traceProperties);
            _logger.TrackPageView("Home");
            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Index View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "HomeController" },
                    {"ExceptionParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"ExceptionParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
                _logger.TrackException(ex, properties);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            _logger.TrackPageView("About");
            var traceProperties = new Dictionary<string, string>()
                {
                    { "Location", "HomeController" },
                    {"TraceParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"TraceParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"TracenParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"TraceParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
            _logger.TrackTrace($"SDK TRACE - Home.About", traceProperties);

            try
            {
                throw new ApplicationException("Dummy Application Exception on About View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "AboutController" },
                    {"ExceptionParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"ExceptionParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameterX", $"Value-{_random.Next(100, 900000)}"  },
                    { "DefaultView", "False" }
                };
                _logger.TrackException(ex, properties);
            }

            return View();
        }

        public ActionResult Contact()
        {
            _logger.TrackPageView("Contact");
            ViewBag.Message = "Your contact page.";
            var traceProperties = new Dictionary<string, string>()
                {
                    { "Location", "HomeController" },
                    {"TraceParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"TraceParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"TracenParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"TraceParameterX", $"Value-{_random.Next(100, 900000)}"  }
                };
            _logger.TrackTrace($"SDK TRACE - Home.Contact", traceProperties);

            try
            {
                throw new ApplicationException("Dummy Application Exception on Contact View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "AboutController" },
                    {"ExceptionParameter1", $"Value-{_random.Next(100, 900000)}" },
                    {"ExceptionParameter2", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameter3", $"Value-{_random.Next(100, 900000)}"  },
                    {"ExceptionParameterX", $"Value-{_random.Next(100, 900000)}"  },
                    { "DefaultView", "False" }
                };
                _logger.TrackException(ex, properties);
            }

            return View();
        }
    }
}