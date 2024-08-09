using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp_AppInsights_SDK_4._8_Framework.Logging
{
    public interface ILogger
    {
        void TrackPageView(string name);
        void TrackTrace(string message, Dictionary<string, string> properties);
        void TrackEvent(string name, Dictionary<string, string> properties);
        void TrackException(Exception exception, Dictionary<string, string> properties);
    }
}
