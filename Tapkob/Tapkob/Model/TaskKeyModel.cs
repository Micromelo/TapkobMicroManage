using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class TaskKeyModel
    {
        [JsonProperty("keys")]
        public List<ItemModel> Keys { get; set; }

        [JsonProperty("map")]
        public MapModel Map { get; set; }
    }
}
