using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class HideoutStationLevelModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonProperty("constructionTime")]
		public int ConstructionTime { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("itemRequirements")]
		public List<RequirementItemModel> ItemRequirements { get; set; }

		[JsonProperty("stationLevelRequirements")]
		public List<RequirementHideoutStationLevelModel> StationLevelRequirements { get; set; }

		[JsonProperty("skillRequirements")]
		public List<RequirementSkillModel> SkillRequirements { get; set; }

		[JsonProperty("traderRequirements")]
		public List<RequirementTraderModel> TraderRequirements { get; set; }

		[JsonProperty("crafts")]
		public List<CraftModel> Crafts { get; set; }
	}
}
