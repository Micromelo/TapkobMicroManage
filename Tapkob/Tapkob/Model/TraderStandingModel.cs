using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class TraderStandingModel
    {
        [JsonProperty("trader")]
        public TraderModel Trader { get; set; }

        [JsonProperty("standing")]
        public double Standing { get; set; }
    }
}
