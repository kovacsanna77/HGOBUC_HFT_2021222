using HGOBUC_HFT_2021222.Models;
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
	public class NetworkTableViewModel : ObservableRecipient
	{

		public RestCollection<Network> networks { get; set; }

		private Network selectedNetwork;

		public Network SelectedNetwork
		{
			get { return selectedNetwork; }
			set
			{
				if (value != null)
				{
					selectedNetwork = new Network()
					{
						NetworkId = value.NetworkId,
						NetworkName = value.NetworkName

					};
				}
				OnPropertyChanged();
				(DeleteNetworkCommand as RelayCommand).NotifyCanExecuteChanged();


			}
		}
		public ICommand CreateNetworkCommand { get; set; }
		public ICommand DeleteNetworkCommand { get; set; }
		public ICommand EditNetworkCommand { get; set; }

		public NetworkTableViewModel()
		{
			networks = new RestCollection<Network>("http://localhost:27826/", "network", "hub");

			CreateNetworkCommand = new RelayCommand(
				() =>
				{
					networks.Add(new Network()
					{
						NetworkName = selectedNetwork.NetworkName
					});
				}
				);
			DeleteNetworkCommand = new RelayCommand(
				() => networks.Delete(selectedNetwork.NetworkId)
				);

			EditNetworkCommand = new RelayCommand(
				() => networks.Update(selectedNetwork)
				);
		}

    }
}
