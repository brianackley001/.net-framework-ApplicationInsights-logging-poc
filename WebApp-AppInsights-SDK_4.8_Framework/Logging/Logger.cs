using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;


namespace WebApp_AppInsights_SDK_4._8_Framework.Logging
{
    public class Logger: ILogger
    {
        private readonly TelemetryClient _telemetryClient;

        public Logger(TelemetryClient telemetryClient)
        {
            if(telemetryClient == null)
            {
                _telemetryClient = new TelemetryClient();
            }
            else
            {
                _telemetryClient = telemetryClient;
            }
        }
        public  void TrackPageView(string name)
        {
            _telemetryClient.TrackPageView(name);
        }

        public  void TrackTrace(string message, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Information, properties);
        }
        public  void TrackEvent(string name, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackEvent(name, properties);
        }
        public  void TrackException(Exception exception, Dictionary<string, string> properties)
        {
            _telemetryClient.TrackException(exception, properties);
        }
    }
}