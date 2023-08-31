using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tapkob.Services;
using Tapkob.ViewModel;

namespace Tapkob.View
{
    /// <summary>
    /// Interaction logic for QuestsView.xaml
    /// </summary>
    public partial class QuestsView : UserControl
    {
        public QuestsView()
        {
            InitializeComponent();
            UpdateTasks();
        }

        private async Task UpdateTasks()
        {
            await TarkovDev.GetTasks();
        }
    }
}
