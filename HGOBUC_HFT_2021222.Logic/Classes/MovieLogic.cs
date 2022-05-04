using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using HGOBUC_HFT_2021222.Repository.ModelRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Classes
{
    public class MovieLogic : IMovieLogic
    {
        IRepository<Movie> repo;
        IRepository<Actors> actorRepo;

        

        public MovieLogic(IRepository<Movie> repo, IRepository<Actors> actorRepo)
        {
            this.repo = repo;
            this.actorRepo = actorRepo;
            
        }

        public void Create(Movie item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Movie Read(int id)
        {
            var movie = this.repo.Read(id);
            if(movie == null)
            {
                throw new ArgumentException("This movie doesn't exists!");
            }
            return movie ;
        }

        public IQueryable<Movie> ReadAll()
        {
            return this.repo.ReadAll();

        }

        public void Update(Movie item)
        {
            this.repo.Update(item);
        }

        //5 NON CRUD
        
        /*public IEnumerable<string> MostMovies()
        {
            return 
        }
               */   

    }

    
}
