﻿using System.Web;
using System.Web.Mvc;

namespace WebApp_NLog_4._8_Framework
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
