using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConsoleApp
{
    public class ExceptionObject
    {
        public Exception Exception { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }
}
