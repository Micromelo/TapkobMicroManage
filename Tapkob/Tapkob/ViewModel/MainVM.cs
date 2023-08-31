using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tapkob.Commands;
using Tapkob.Utilities;

namespace Tapkob.ViewModel
{
    public class MainVM : BaseVM
    {
        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get { return _currentVM; }
            set 
            { 
                _currentVM = value;
                OnPropertyChanged(nameof(CurrentVM));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainVM()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
