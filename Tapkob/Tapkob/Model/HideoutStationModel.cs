using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class HideoutStationModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("normalizedName")]
		public string NormalizedName { get; set; }

		[JsonProperty("levels")]
		public List<HideoutStationLevelModel> Levels { get; set; }

		[JsonProperty("tarkovDataId")]
		public int? TarkovDataId { get; set; }

		[JsonProperty("crafts")]
		public List<CraftModel> Crafts { get; set; }

	}
}
