using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels
{
    public class Actorswith10WindowViewModel : ObservableRecipient
    {
        List<string> actorswith10ratings { get; set; }
        RestService rest;
        string selectedNonCrud { get; set; }

        public List<string> ActorsWith10ratings
        {
            get { return rest.Get<string>("Stat/ActorsWith10Rating"); }
        }

        public Actorswith10WindowViewModel()
        {
            rest = new RestService("http://localhost:27826/");

            if (selectedNonCrud == "ActorsWith10ratings")
            {
                actorswith10ratings = new List<string>();
                actorswith10ratings = ActorsWith10ratings.ToList();
            }

        }

        public void SetSelected(string s)
        {
            selectedNonCrud = s;
        }
    }
}
