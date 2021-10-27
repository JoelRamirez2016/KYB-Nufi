using System.Collections.Generic;

namespace Nufi.kyb.v2.Models
{
    public class IMPIData
    {
        public string name { get; set; }
        public string fileNumber { get; set; }
        public string requestType { get; set; }
        public string headline { get; set; }
        public string status { get; set; }
        public Dictionary<string, string> generalData { get; set; }
        public Dictionary<string, string> headlineData { get; set; }
        public Dictionary<string, string>[] productsAndServices { get; set; }
        public Dictionary<string, string>[] procedures { get; set; }
    }
} 
