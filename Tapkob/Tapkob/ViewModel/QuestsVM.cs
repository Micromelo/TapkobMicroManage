using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Model;
using Tapkob.Services;
using Tapkob.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Tapkob.ViewModel
{
    public class QuestsVM : BaseVM
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
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
                OnPropertyChanged();
            }
        }
        public ObservableCollection<TaskObjectiveBasicModel> TaskObjectives { get; set; }
        public ObservableCollection<ItemModel> TaskKeys { get; set; }
        public ObservableCollection<TaskModel> TaskRewards { get; set; }

        public QuestsVM()
        {
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasks();
        }

        public void LoadTasks()
        {
            Tasks = TarkovDev.Tasks;
        }
    }
}
