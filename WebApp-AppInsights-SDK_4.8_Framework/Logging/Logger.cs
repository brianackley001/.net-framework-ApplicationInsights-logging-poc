using Microsoft.Ajax.Utilities;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;


namespace WebApp_AppInsights_SDK_4._8_Framework.Logging
{
    public class Logger: ILogger
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly TelemetryConfiguration _telemetryConfiguration;

        public Logger(TelemetryClient telemetryClient, TelemetryConfiguration telemetryConfiguration)
        {
            if(telemetryClient == null || _telemetryConfiguration == null)
            {
                _telemetryClient = new TelemetryClient(); 
                this._telemetryConfiguration = telemetryConfiguration;
                this._telemetryClient.TelemetryConfiguration.ConnectionString = WebConfigurationManager.AppSettings["applicationInsightsConnectionString"];
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