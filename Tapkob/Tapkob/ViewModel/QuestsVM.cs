using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ObservableCollection<TaskModel> Tasks { get; set; }
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
                selectedTask = value;
                UpdateTaskObjectives(value);
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

            public TaskObjectiveDescription(string description, string subDescription)
            {
                Description = description;
                SubDescription = subDescription;
            }
        }

        public QuestsVM()
        {
            Tasks = new ObservableCollection<TaskModel>();
            Items = new ObservableCollection<ItemModel>();
            LoadTasks();
            LoadItems();
        }

        public void LoadTasks()
        {
            Tasks = TarkovDev.Tasks;
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
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveBuildItem taskObjectiveBuildItem:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveExperience taskObjectiveExperience:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveExtract taskObjectiveExtract:
                            if (taskObjectiveExtract.ExitName != String.Empty)
                                subDescription = "Extract at: " + taskObjectiveExtract.ExitName;
                            else
                                subDescription = string.Empty;
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveItem taskObjectiveItem:
                            subDescription = "Needed: " + taskObjectiveItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveMark taskObjectiveMark:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectivePlayerLevel taskObjectivePlayerLevel:
                            subDescription = "Required Level: " + taskObjectivePlayerLevel.PlayerLevel.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveQuestItem taskObjectiveQuestItem:
                            subDescription = "Amount: " + taskObjectiveQuestItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveShoot taskObjectiveShoot:
                            subDescription = "Amount: " + taskObjectiveShoot.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveSkill taskObjectiveSkill:
                            subDescription = "Required Level: " + taskObjectiveSkill.SkillLevel.Level.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveTaskStatus taskObjectiveTaskStatus:
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveTraderLevel taskObjectiveTraderLevel:
                            subDescription = "Required Level: " + taskObjectiveTraderLevel.Level.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;

                        case TaskObjectiveUseItem taskObjectiveUseItem:
                            subDescription = "Amount: " + taskObjectiveUseItem.Count.ToString();
                            currentSelectedTaskObjectives.Add(new TaskObjectiveDescription(description, subDescription));
                            break;
                    }
                }
            }
            SelectedTaskObjectiveDescriptions = currentSelectedTaskObjectives;
        }

        private bool IncludeObjective(ITaskObjective objective)
        {
            bool includeObjective = true;

            switch (objective.Type)
            {
                case "giveQuestItem":
                    includeObjective = false;
                    break;
            }
            
            return includeObjective;
        }
    }
}
