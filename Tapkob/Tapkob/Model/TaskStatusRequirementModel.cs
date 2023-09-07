using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class TaskStatusRequirementModel
    {
        [JsonProperty("task")]
        public TaskModel Task { get; set; }

        [JsonProperty("status")]
        public List<string> Status { get; set; }
    }
}
