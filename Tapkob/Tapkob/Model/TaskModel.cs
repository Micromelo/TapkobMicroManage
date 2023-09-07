using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tapkob.Interfaces;

namespace Tapkob.Model
{
    public class TaskModel
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

        [JsonProperty("trader")]
        public TraderModel Trader { get; set; }

        [JsonProperty("map")]
        public MapModel Map { get; set; }

        [JsonProperty("experience")]
        public int Experience { get; set; }

        [JsonProperty("wikiLink")]
        public string WikiLink { get; set; }

        [JsonProperty("minPlayerLevel")]
        public int? MinPlayerLevel { get; set; }

        [JsonProperty("taskRequirements")]
        public List<TaskStatusRequirementModel> TaskRequirements { get; set; }

        [JsonProperty("traderLevelRequirements")]
        public List<RequirementTraderModel> TraderLevelRequirements { get; set; }

        [JsonProperty("objectives")]
        public List<ITaskObjective> Objectives { get; set; }

        [JsonProperty("finishRewards")]
        public TaskRewardModel TaskRewards { get; set; }

        [JsonProperty("neededKeys")]
        public List<TaskKeyModel> NeededKeys { get; set; }

        [JsonProperty("kappaRequired")]
        public bool KappaRequired { get; set; }

        [JsonProperty("lightkeeperRequired")]
        public bool LightkeeperRequired { get; set; }

        public bool IsCompleted { get; set; }
	}
}
