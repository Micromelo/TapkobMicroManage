using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class SkillLevelModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("level")]
        public double Level { get; set; }
    }
}
