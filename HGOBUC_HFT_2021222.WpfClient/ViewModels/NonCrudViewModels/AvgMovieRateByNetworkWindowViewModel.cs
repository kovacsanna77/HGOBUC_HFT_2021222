using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels
{
    public class AvgMovieRateByNetworkWindowViewModel
    {
        IEnumerable<KeyValuePair<string, double>> avgMovieRateByNetwork { get; set; }
        RestService rest;

        public IEnumerable<KeyValuePair<string, double>> getAvgMovieRateByNetwork
        {
            get { return rest.Get<KeyValuePair<string, double>>("Stat/AvgMovieRateByNetwork"); }
        }

        public AvgMovieRateByNetworkWindowViewModel()
        {
            rest = new RestService("http://localhost:27826/");

            avgMovieRateByNetwork = getAvgMovieRateByNetwork;

        }
    }
}
