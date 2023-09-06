using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tapkob.Model
{
    public class TaskRewardModel
    {
		[JsonProperty("traderStanding")]
		public List<TraderStandingModel> TraderStanding { get; set; }

		[JsonProperty("items")]
		public List<ContainedItemModel> Items { get; set; }

		[JsonProperty("offerUnlock")]
		public List<OfferUnlockModel> OfferUnlock { get; set; }

		[JsonProperty("skillLevelReward")]
		public List<SkillLevelModel> SkillLevelReward { get; set; }

		[JsonProperty("traderUnlock")]
		public List<TraderModel> TraderUnlock { get; set; }

		[JsonProperty("craftUnlock")]
		public List<CraftModel> CraftUnlock { get; set; }
	}
}
