using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Interfaces;

namespace Tapkob.Model
{
    public class TaskObjectiveExperienceModel : ITaskObjective
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

		[JsonProperty("healthEffect")]
		public HealthEffectModel HealthEffect { get; set; }
	}
}
