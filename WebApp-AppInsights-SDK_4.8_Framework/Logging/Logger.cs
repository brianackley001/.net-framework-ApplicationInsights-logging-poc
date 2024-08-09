using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;


namespace WebApp_AppInsights_SDK_4._8_Framework.Logging
{
    public class Logger: ILogger
    {
        // Global static instance for App Insights
        private static TelemetryClient _AppInsights;
        private static TelemetryClient AppInsights
        {
            get
            {
                if (_AppInsights == null) { _AppInsights = new TelemetryClient(); }
                return _AppInsights;
            }
        }

        public Logger(TelemetryClient appInsights)
        {
        }
        public  void TrackPageView(string name)
        {
            AppInsights.TrackPageView(name);
        }

        public  void TrackTrace(string message, Dictionary<string, string> properties)
        {
            AppInsights.TrackTrace(message, SeverityLevel.Information, properties);
        }
        public  void TrackEvent(string name, Dictionary<string, string> properties)
        {
            AppInsights.TrackEvent(name, properties);
        }
        public  void TrackException(Exception exception, Dictionary<string, string> properties)
        {
            AppInsights.TrackException(exception, properties);
        }
    }
}