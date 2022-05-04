using HGOBUC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Interfaces
{
   public interface IMovieLogic
    {
        void Create(Movie item);
        Movie Read(int id);
        void Update(Movie item);
        void Delete(int id);
        IQueryable<Movie> ReadAll();
        IEnumerable<string> MostMovies();
    }
}
