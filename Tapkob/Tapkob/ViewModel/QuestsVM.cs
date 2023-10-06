using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Tapkob.Commands;
using Tapkob.Interfaces;
using Tapkob.Model;
using Tapkob.Model.TaskObjective;
using Tapkob.Services;
using Tapkob.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Tapkob.ViewModel
{
    public class QuestsVM : BaseVM
    {
        public ICommand ChangeTraderCommand { get; set; }

        private ObservableCollection<TaskModel> traderTasks { get; set; }
        public ObservableCollection<TaskModel> TraderTasks 
        {
            get 
            {
                return traderTasks;
            } 
            set 
            {
                traderTasks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ItemModel> Items { get; set; }

        private TaskModel selectedTask;
        public TaskModel SelectedTask 
        {
            get
            {
                return selectedTask;
            }
            set 
            {
                if (value == null) 
                {
                    if (TraderTasks[0] != null)
                    {
                        selectedTask = TraderTasks[0];
                    }
                    else 
                    {
                        selectedTask = new TaskModel();
                    }
                }
                else
                {
                    selectedTask = value;
                }
                
                UpdateTaskObjectives(selectedTask);
                UpdateTaskNeededKeys(selectedTask.NeededKeys);
                UpdateTaskRewards(selectedTask.TaskRewards);

                OnPropertyChanged();
            }
        }

        private List<TaskObjectiveDescription> selectedTaskObjectiveDescriptions;
        public List<TaskObjectiveDescription> SelectedTaskObjectiveDescriptions
        {
            get
            {
                return selectedTaskObjectiveDescriptions;
            }
            set
            {
                selectedTaskObjectiveDescriptions = value;
                OnPropertyChanged();
            }
        }

        public class TaskObjectiveDescription
        {
            public string Description { get; set; }
            public string SubDescription { get; set; }
            public string ObjectiveIconPath { get; set; }

            public TaskObjectiveDescription(string description, string subDescription, string objectiveIconPath)
            {
                Description = description;
                SubDescription = subDescription;
                ObjectiveIconPath = objectiveIconPath;
            }
        }

        private List<ItemModel> selectedTaskNeededKeys { get; set; }
        public List<ItemModel> SelectedTaskNeededKeys
        {
            get
            {
                return selectedTaskNeededKeys;
            }
            set
            {
                selectedTaskNeededKeys = value;
                OnPropertyChanged();
            }
        }

        private List<TaskRewards> selectedTaskRewards { get; set; }
        public List<TaskRewards> SelectedTaskRewards
        {
            get
            {
                return selectedTaskRewards;
            }
            set
            {
                selectedTaskRewards = value;
                OnPropertyChanged();
            }
        }

        public class TaskRewards
        {
            public string Description { get; set; }
            public string SubDescription { get; set; }
            public string WikiLink { get; set; }
            public string BaseImageLink { get; set; }

            public TaskRewards(string description, string subDescription, string wikiLink, string baseImageLink)
            {
                Description = description;
                SubDescription = subDescription;
                WikiLink = wikiLink;
                BaseImageLink = baseImageLink;
            }
        }

        public QuestsVM()
        {
            ChangeTraderCommand = new RelayCommand(ChangeTrader,CanChangeTrader);
            TraderTasks = new ObservableCollection<TaskModel>();
            Items = new ObservableCollection<ItemModel>();
            LoadTasks();
            LoadItems();
        }

        public void LoadTasks()
        {
            TraderTasks = TarkovDev.Tasks;
        }

        public void LoadTasks(string trader)
        {
            ObservableCollection<TaskModel> filteredTasks = new ObservableCollection<TaskModel>();
            foreach (TaskModel task in TarkovDev.Tasks)
            {
                if (task.Trader.Name == trader)
                {
                    filteredTasks.Add(task);
                }
            }

            TraderTasks = filteredTasks;
        }
        public void LoadItems()
        {
            Items = TarkovDev.Items;
        }

        private void UpdateTaskObjectives(TaskModel currentSelectedTask)
        {
            List<TaskObjectiveDescription> currentSelectedTaskObjectives = new List<TaskObjectiveDescription>();
            string description = string.Empty;
            string subDescription = string.Empty;

            foreach (ITaskObjective taskObjective in currentSelectedTask.Objectives) 
            {
                if (IncludeObjective(taskObjective))
                {
                    description = string.Empty;
                    subDescription = string.Empty;

                    if (taskObjective.Optional)
                        description = taskObjective.Description + " (OPTIONAL) ";
                    else
                        description = taskObjective.Description;

                    switch (taskObjective)
                    {
                        case TaskObjectiveBasic taskObjectiveBasic:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveBasic.Type)));
                            break;

                        case TaskObjectiveBuildItem taskObjectiveBuildItem:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveBuildItem.Type)));
                            break;

                        case TaskObjectiveExperience taskObjectiveExperience:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveExperience.Type)));
                            break;

                        case TaskObjectiveExtract taskObjectiveExtract:
                            if (taskObjectiveExtract.ExitName != String.Empty & taskObjectiveExtract.ExitName != null)
                                subDescription = "Extract at: " + taskObjectiveExtract.ExitName;
                            else
                                subDescription = string.Empty;
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveExtract.Type)));
                            break;

                        case TaskObjectiveItem taskObjectiveItem:
                            subDescription = "Needed: " + taskObjectiveItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveItem.Type)));
                            break;

                        case TaskObjectiveMark taskObjectiveMark:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveMark.Type)));
                            break;

                        case TaskObjectivePlayerLevel taskObjectivePlayerLevel:
                            subDescription = "Required Level: " + taskObjectivePlayerLevel.PlayerLevel.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectivePlayerLevel.Type)));
                            break;

                        case TaskObjectiveQuestItem taskObjectiveQuestItem:
                            subDescription = "Amount: " + taskObjectiveQuestItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveQuestItem.Type)));
                            break;

                        case TaskObjectiveShoot taskObjectiveShoot:
                            subDescription = "Amount: " + taskObjectiveShoot.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveShoot.Type)));
                            break;

                        case TaskObjectiveSkill taskObjectiveSkill:
                            subDescription = "Required Level: " + taskObjectiveSkill.SkillLevel.Level.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveSkill.Type)));
                            break;

                        case TaskObjectiveTaskStatus taskObjectiveTaskStatus:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveTaskStatus.Type)));
                            break;

                        case TaskObjectiveTraderLevel taskObjectiveTraderLevel:
                            subDescription = "Required Level: " + taskObjectiveTraderLevel.Level.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveTraderLevel.Type)));
                            break;

                        case TaskObjectiveUseItem taskObjectiveUseItem:
                            subDescription = "Amount: " + taskObjectiveUseItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription, GetTaskObjectiveIconPath(taskObjectiveUseItem.Type)));
                            break;
                    }
                }
            }
            SelectedTaskObjectiveDescriptions = currentSelectedTaskObjectives;
        }

        private string GetTaskObjectiveIconPath(string objectiveType)
        {
            string taskObjectiveIconPath = string.Empty;

            switch (objectiveType)
            {
                case "buildWeapon":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_gun.png";
                    break;
                case "shoot":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_eliminate.png";
                    break;
                case "findItem":
                case "findQuestItem":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_search.png";
                    break;
                case "giveItem":
                case "giveQuestItem":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_give.png";
                    break;
                case "skill":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_skill.png";
                    break;
                case "visit":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_location.png";
                    break;
                case "experience":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_time.png";
                    break;
                case "extract":
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_escape.png";
                    break;
                default:
                    taskObjectiveIconPath = "/Resources/Icons/icon_objective_search.png";
                    break;
            }

            return taskObjectiveIconPath;
        }

        private bool IncludeObjective(ITaskObjective objective)
        {
            bool includeObjective = true;

            //switch (objective.Type)
            //{
            //    case "giveQuestItem":
            //        includeObjective = false;
            //        break;
            //}
            
            return includeObjective;
        }

        private void UpdateTaskNeededKeys(List<TaskKeyModel> currentNeededKeys)
        {
            List<ItemModel> currentSelectedTaskNeededKeys = new List<ItemModel>();

            foreach (TaskKeyModel neededKey in currentNeededKeys) 
            { 
                foreach(ItemModel key in neededKey.Keys)
                {
                    currentSelectedTaskNeededKeys.Add(key);
                }
            }

            SelectedTaskNeededKeys = currentSelectedTaskNeededKeys;
        }

        private void UpdateTaskRewards(TaskRewardModel currentRewards)
        {
            List<TaskRewards> currentSelectedTaskRewards = new List<TaskRewards>();
            string subDescription = string.Empty;
            string baseImageLink = string.Empty;

            foreach (TraderStandingModel traderStanding in currentRewards.TraderStanding)
            {
                if (traderStanding.Standing > 0)
                    subDescription = "+" + traderStanding.Standing.ToString();
                else
                    subDescription = traderStanding.Standing.ToString();
                baseImageLink = "/Resources/Icons/icon_trader_" + traderStanding.Trader.Name.ToLower() + ".png";

                currentSelectedTaskRewards.Add(new TaskRewards(traderStanding.Trader.Name, subDescription, "", baseImageLink));
            }
            foreach (ContainedItemModel containedItem in currentRewards.Items)
            {
                if (containedItem.Item.Name == "Roubles")
                    subDescription = String.Format("{0:N0}", containedItem.Count) + "₽";
                else if (containedItem.Item.Name == "Dollars")
                    subDescription = "$" + String.Format("{0:N0}", containedItem.Count);
                else if (containedItem.Item.Name == "Euros")
                    subDescription = "€" + String.Format("{0:N0}", containedItem.Count);
                else
                    subDescription = "x" + containedItem.Count.ToString();
                baseImageLink = containedItem.Item.BaseImageLink;

                currentSelectedTaskRewards.Add(new TaskRewards(containedItem.Item.Name, subDescription, containedItem.Item.WikiLink, baseImageLink));
            }
            foreach (OfferUnlockModel offerUnlock in currentRewards.OfferUnlock)
            {
                subDescription = "(" + offerUnlock.Trader.Name + " LL" + offerUnlock.Level.ToString() + ")";
                baseImageLink = offerUnlock.Item.BaseImageLink;

                currentSelectedTaskRewards.Add(new TaskRewards(offerUnlock.Item.Name, subDescription, offerUnlock.Item.WikiLink, baseImageLink));
            }
            foreach (SkillLevelModel skillLevel in currentRewards.SkillLevelReward)
            {
                subDescription = "+" + skillLevel.Level.ToString() + " Level(s)";
                baseImageLink = "path";

                currentSelectedTaskRewards.Add(new TaskRewards(skillLevel.Name, subDescription, "", baseImageLink));
            }
            foreach (TraderModel trader in currentRewards.TraderUnlock)
            {
                subDescription = "Unlocked";
                baseImageLink = "/Resources/Icons/icon_trader_" + trader.Name.ToLower() + ".png";

                currentSelectedTaskRewards.Add(new TaskRewards(trader.Name, subDescription, "", baseImageLink));
            }

            SelectedTaskRewards = currentSelectedTaskRewards;
        }

        private void ChangeTrader(object obj)
        {
            string trader = string.Empty;

            switch (obj.ToString())
            {
                case "PraporQuestButton":
                    trader = "Prapor";
                    break;
                case "TherapistQuestButton":
                    trader = "Therapist";
                    break;
                case "SkierQuestButton":
                    trader = "Skier";
                    break;
                case "PeacekeeperQuestButton":
                    trader = "Peacekeeper";
                    break;
                case "MechanicQuestButton":
                    trader = "Mechanic";
                    break;
                case "RagmanQuestButton":
                    trader = "Ragman";
                    break;
                case "JaegerQuestButton":
                    trader = "Jaeger";
                    break;
                case "FenceQuestButton":
                    trader = "Fence";
                    break;
                case "LightkeeperQuestButton":
                    trader = "Lightkeeper";
                    break;
            }

            LoadTasks(trader);
        }

        private bool CanChangeTrader(object obj)
        {
            return true;
        }
    }
}
