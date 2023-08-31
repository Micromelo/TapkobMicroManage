using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tapkob.ViewModel;

namespace Tapkob.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainVM viewModel;

        public event EventHandler CanExecuteChanged;

        public UpdateViewCommand(MainVM vM)
        {
            viewModel = vM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Home")
            {
                viewModel.CurrentVM = new HomeVM();
            } 
            else if (parameter.ToString() == "Quests")
            {
                viewModel.CurrentVM = new QuestsVM();
            }
        }
    }
}
