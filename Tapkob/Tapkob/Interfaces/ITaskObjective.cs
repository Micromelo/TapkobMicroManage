using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tapkob.Model;

namespace Tapkob.Interfaces
{
    public interface ITaskObjective
    {
		[JsonProperty("id")]
		string Id { get; set; }

		[JsonProperty("type")]
		string Type { get; set; }

		[JsonProperty("description")]
		string Description { get; set; }

		[JsonProperty("maps")]
		List<MapModel> Maps { get; set; }

		[JsonProperty("optional")]
		bool Optional { get; set; }
	}
}
