using System;

namespace Tapkob.Model
{
    public class TaskModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool isCompleted { get; set; }
        public TraderModel trader { get; set; }
        public MapModel map { get; set; }
        public int experience {get; set; }
        public string wikiLink { get; set; }
        public string imageLink { get; set; }
        public int minPlayerLevel { get; set; }
        public TaskModel[] taskRequirements { get; set; }
        public TaskObjectiveModel[] taskObjectives { get; set; }
        public TaskRewardModel taskRewards { get; set; }
        public TaskKeyModel[] taskKeys { get; set; }
        public bool kappaRequired { get; set; }
        public bool lightkeeperRequired { get; set; }
    }
}
