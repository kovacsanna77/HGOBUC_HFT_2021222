using System.Collections.Generic;

namespace HGOBUC_HFT_2021222.WpfClient.ViewModels.NonCrudViewModels
{
    public class MoviesWith10RatingWindowViewModel
    {


        IEnumerable<KeyValuePair<string, string>> moviesWith10Actor { get; set; }
        RestService rest;

        public IEnumerable<KeyValuePair<string, string>> getMovieswith10
        {
            get { return rest.Get<KeyValuePair<string, string>>("Stat/MoviesWith10Rating"); }
        }

        public MoviesWith10RatingWindowViewModel()
        {
            rest = new RestService("http://localhost:27826/");

            moviesWith10Actor = getMovieswith10;
        }
    }
}
