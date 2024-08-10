using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestVolumeLogging_ConoleApp
{
    public class PostPayload
    {
        public string Id { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public string Name { get; set; }
    }
}
