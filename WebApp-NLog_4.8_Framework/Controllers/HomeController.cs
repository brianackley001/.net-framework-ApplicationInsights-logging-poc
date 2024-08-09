using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp_NLog_4._8_Framework.Controllers
{
    public class HomeController : Controller
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();



        [HttpPost]
        public ActionResult TrackEvent()
        {
            string formValue = Request.Form["Text1"];
            _logger.Log(new LogEventInfo(LogLevel.Info, "Default", formValue));
            return View("Index");
        }

        public ActionResult Index()
        {
            _logger.Trace("HomeController.Index");

            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Index View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    {"Property1", "Test Value for HomeController.Index" },
                    {"Property2", "Another Test Value for HomeController.Index" }
                };
                _logger.Error(ex, ex.Message, properties);
            }
            _logger.Debug("HomeController, Index View DEBUG statement");
            _logger.Warn("HomeController, Index View WARN statement");
            return View();
        }

        public ActionResult About()
        {
            _logger.Trace("HomeController.About");

            try
            {
                throw new ArgumentOutOfRangeException("Dummy Argument Out of Range Exception on Index View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    {"Property1", "Test Value for HomeController.About" },
                    {"Property2", "Another Test Value for HomeController.About" }
                };
                _logger.Error(ex, ex.Message, properties);
            }
            _logger.Debug("HomeController, About View DEBUG statement");
            _logger.Warn("HomeController, About View WARN statement");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            _logger.Trace("HomeController.Contact");

            try
            {
                throw new ApplicationException("Dummy Application Exception on Index View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    {"Property1", "Test Value for HomeController.Contact" },
                    {"Property2", "Presumed Business Rule conflict or violation" }
                };
                _logger.Error(ex, ex.Message, properties);
            }
            _logger.Debug("HomeController, Contact View DEBUG statement");
            _logger.Warn("HomeController, Contact View WARN statement");
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}