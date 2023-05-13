using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels
{
    public class MainWindowViewModel: ObservableRecipient
    {
       public ICommand MoviesTableCommand { get; set; }
       public ICommand NetworkTableCommand { get; set; }
       public  ICommand RolesTableCommand { get; set; }
      


        public MainWindowViewModel()
        {

            MoviesTableCommand = new RelayCommand(
                () => OpenMovies()

                );
            RolesTableCommand = new RelayCommand(
                () => OpenRoles());

            NetworkTableCommand = new RelayCommand(
                ()=> OpenNetwork()
                );
        }

        public void OpenMovies()
        {
            new MoviesTable().ShowDialog();
        }
        public void OpenRoles()
        {
            new RolesTableWindow().ShowDialog();
        }

        public void OpenNetwork()
        {
            new NetworkTableWindow().ShowDialog();
        }
    }
}
