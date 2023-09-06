using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class CraftModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("station")]
		public HideoutStationModel Station { get; set; }

		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonProperty("taskUnlock")]
		public Task TaskUnlock { get; set; }

		[JsonProperty("duration")]
		public int Duration { get; set; }

		[JsonProperty("requiredItems")]
		public List<ContainedItemModel> RequiredItems { get; set; }

		[JsonProperty("requiredQuestItems")]
		public List<TaskQuestItemModel> RequiredQuestItems { get; set; }

		[JsonProperty("rewardItems")]
		public List<ContainedItemModel> RewardItems { get; set; }
	}
}
