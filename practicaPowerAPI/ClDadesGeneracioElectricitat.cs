using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace practicaPowerAPI
{
    internal class ClDadesGeneracioElectricitat
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Attributes
        {
            public string title { get; set; }

            [JsonProperty("last-update")]
            public object LastUpdate { get; set; }
            public object description { get; set; }
            public string color { get; set; }
            public string type { get; set; }
            public object magnitude { get; set; }
            public bool composite { get; set; }
            public List<Value> values { get; set; }
        }

        public class CacheControl
        {
            public string cache { get; set; }
            public DateTime expireAt { get; set; }
        }

        public class Data
        {
            public string type { get; set; }
            public string id { get; set; }
            public Attributes attributes { get; set; }
            public Meta meta { get; set; }
        }

        public class Included
        {
            public string type { get; set; }
            public string id { get; set; }
            public string groupId { get; set; }
            public Attributes attributes { get; set; }
        }

        public class Meta
        {
            [JsonProperty("cache-control")]
            public CacheControl CacheControl { get; set; }
        }

        public class RootGeneracio
        {
            public Data data { get; set; }
            public List<Included> included { get; set; }
        }

        public class Value
        {
            public double value { get; set; }
            public double percentage { get; set; }
            public DateTime datetime { get; set; }
        }
    }
}
