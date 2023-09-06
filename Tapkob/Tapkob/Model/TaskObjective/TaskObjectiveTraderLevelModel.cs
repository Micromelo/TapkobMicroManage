using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Interfaces;

namespace Tapkob.Model.TaskObjective
{
    public class TaskObjectiveTraderLevelModel : ITaskObjective
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

		[JsonProperty("trader")]
		public TraderModel Trader { get; set; }

		[JsonProperty("level")]
		public int Level { get; set; }
	}
}
