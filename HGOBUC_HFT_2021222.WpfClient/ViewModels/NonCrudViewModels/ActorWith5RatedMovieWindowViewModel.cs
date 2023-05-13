using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels
{
    public  class ActorWith5RatedMovieWindowViewModel
    {
        IEnumerable<KeyValuePair<string, string>> actorswith5rating { get; set; }
        RestService rest;

        public IEnumerable<KeyValuePair<string, string>> getActorswith5
        {
            get { return rest.Get<KeyValuePair<string, string>>("Stat/ActorWith5RatedMovie"); }
        }

        public ActorWith5RatedMovieWindowViewModel()
        {
            rest = new RestService("http://localhost:27826/");

           actorswith5rating = getActorswith5;

        }
    }
}
