using log4net.Repository.Hierarchy;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net.Config;

namespace WebApp_Log4Net_4._8_Framework.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //log4net.LogManager.GetLogger("default"); 


        [HttpPost]
        public ActionResult TrackEvent()
        {
            string formValue = Request.Form["Text1"];
            //Logger.TrackEvent(formValue);
            log.Info(formValue);
            return View("Index");
        }


        public ActionResult Index()
        {
            // Configure log4net using the config file
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            log.Info("HomeController, Index View loaded");

            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Index View");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            log.Debug("HomeController, Index View DEBUG statement");
            log.Warn("HomeController, Index View WARN statement");

            return View();
        }

        public ActionResult About()
        {
            log.Info("HomeController, About View loaded");

            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Index View");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            log.Debug("HomeController, About View DEBUG statement");
            log.Warn("HomeController, About View WARN statement");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            log.Info("HomeController, Contact View loaded");

            try
            {
                throw new NullReferenceException("Dummy Null Refrence Exception on Contact View");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            log.Debug("HomeController, Contact View DEBUG statement");
            log.Warn("HomeController, Contact View WARN statement");

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}