using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.ApplicationInsights;
using WebApp_AppInsights_SDK_4._8_Framework.Logging;


namespace WebApp_AppInsights_SDK_4._8_Framework.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult TrackEvent()
        {
            string formValue = Request.Form["Text1"];
            Logger.TrackEvent(formValue);
            return View("Index");
        }

        public void Event_Btn_Click(Object sender,
                               EventArgs e)
        {
            Logger.TrackEvent(e.ToString());
        }
        public ActionResult Index()
        {
            Logger.TrackPageView("Home");
            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Index View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "HomeController" }
                };
                Logger.TrackException(ex, properties);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Logger.TrackPageView("About");

            try
            {
                throw new ApplicationException("Dummy Application Exception on About View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "AboutController" },
                    { "DefaultView", "False" }
                };
                Logger.TrackException(ex, properties);
            }

            return View();
        }

        public ActionResult Contact()
        {
            Logger.TrackPageView("Contact");
            ViewBag.Message = "Your contact page.";

            try
            {
                throw new ApplicationException("Dummy Application Exception on Contact View");
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "Location", "AboutController" },
                    { "DefaultView", "False" }
                };
                Logger.TrackException(ex, properties);
            }

            return View();
        }
    }
}