using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels
{
    public class AvgEpisodesPerNetworkWindowViewModel
    {
        IEnumerable<KeyValuePair<string,double>> avgEpPerNetwork{ get; set; }
        RestService rest;
        string selectedNonCrud { get; set; }

        public IEnumerable<KeyValuePair<string,double>> getAvgEpPerNetwork
        {
            get { return rest.Get<KeyValuePair<string, double>>("Stat/AvgEpisodesPerNetwork"); }
        }

        public AvgEpisodesPerNetworkWindowViewModel()
        {
            rest = new RestService("http://localhost:27826/");

            avgEpPerNetwork = getAvgEpPerNetwork;

        }

        public void SetSelected(string s)
        {
            selectedNonCrud = s;
        }
    }
}
