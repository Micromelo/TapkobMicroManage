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
        public BaseVM CurrentVM { get; set; }

        public MainVM()
        {
            CurrentVM = new HomeVM();
        }
    }
}
