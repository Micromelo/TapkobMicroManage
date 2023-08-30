using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapkob.Utilities;

namespace Tapkob.ViewModel
{
    internal class MainVM : BaseVM
    {
        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get { return _currentVM; }
            set { _currentVM = value; }
        }
    }
}
