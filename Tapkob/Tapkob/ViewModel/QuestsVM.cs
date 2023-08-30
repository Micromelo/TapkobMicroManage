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
    internal class QuestsVM : BaseVM
    {
        public ObservableCollection<TaskModel> Tasks = new ObservableCollection<TaskModel>();

        public QuestsVM()
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            Tasks = TarkovDev.Tasks;
        }
    }
}
