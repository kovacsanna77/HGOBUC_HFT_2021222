using HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels;
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
using System.Windows.Shapes;

namespace HGOBUC_HFT_2021222.WpfClient
{
    /// <summary>
    /// Interaction logic for NoncrudWindow.xaml
    /// </summary>
    public partial class NoncrudWindow : Window
    {
        public NoncrudWindow(string selected)
        {
            InitializeComponent();

            string current = selected;
            //var ViewModel = new NoncrudWindowViewModel();
            //ViewModel.SetSelected(current);

            (DataContext as Actorswith10WindowViewModel).SetSelected(current);

        }
    }
}
