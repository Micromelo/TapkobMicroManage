using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Interfaces;

namespace Tapkob.Model.TaskObjective
{
    public class TaskObjectiveExtract : ITaskObjective
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("maps")]
		public List<MapModel> Maps { get; set; }

		[JsonProperty("optional")]
		public bool Optional { get; set; }

		[JsonProperty("exitStatus")]
		public List<string> ExitStatus { get; set; }

		[JsonProperty("exitName")]
		public string ExitName { get; set; }

		[JsonProperty("zoneNames")]
		public List<string> ZoneNames { get; set; }
	}
}
