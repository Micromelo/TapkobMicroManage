using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class NumberCompareModel
    {
        [JsonProperty("compareMethod")]
        public string CompareMethod { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
