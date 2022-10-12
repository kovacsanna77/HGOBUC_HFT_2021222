using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        ICommand MoviesTable { get; set; }
        ICommand ActrosTable { get; set; }
        ICommand RolesTable { get; set; }


        public MainWindowViewModel()
        {

        }
    }
}
