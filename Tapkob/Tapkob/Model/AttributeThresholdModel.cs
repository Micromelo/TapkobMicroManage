using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class AttributeThresholdModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("requirement")]
        public NumberCompareModel Requirement { get; set; }
    }
}
