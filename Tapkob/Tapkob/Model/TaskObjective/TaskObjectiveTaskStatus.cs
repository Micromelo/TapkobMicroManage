using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Interfaces;

namespace Tapkob.Model.TaskObjective
{
    public class TaskObjectiveTaskStatus : ITaskObjective
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

		[JsonProperty("task")]
		public TaskModel Task { get; set; }

		[JsonProperty("status")]
		public List<string> Status { get; set; }
	}
}
