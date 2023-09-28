using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tapkob.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> ExecuteAction { get; set; }
        private Predicate<object> CanExecuteAction { get; set; }

        public RelayCommand(Action<object> executeAction, Predicate<object> canExecute)
        {
            ExecuteAction = executeAction;
            CanExecuteAction = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
    }
}
