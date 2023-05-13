using HGOBUC_HFT_2021222.Models;
using System.Collections.Generic;

namespace HGOBUC_HFT_2021222.Logic.Interfaces
{
    public interface IMovieLogic
    {
        void Create(Movie item);
        Movie Read(int id);
        void Update(Movie item);
        void Delete(int id);
        List<string> getListofNonCruds();
        IEnumerable<Movie> ReadAll();
        IEnumerable<KeyValuePair<string, double?>> AvgEpisodesPerNetwork();
        IEnumerable<KeyValuePair<string, double?>> AvgMovieRateByNetwork();
        IEnumerable<KeyValuePair<string, string>> MoviesWith10RatingWithMainActor();
        IEnumerable<KeyValuePair<string, string>> ActorWith5RatedMovie();
        IEnumerable<string> ActorsWith10Rating();
    }
}
