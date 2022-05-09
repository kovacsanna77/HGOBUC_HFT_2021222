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
        IRepository<Role> roleRepo;
        IRepository<Network> networkRepo;

        public MovieLogic(IRepository<Movie> repo)
        {
            this.repo = repo;
        }

        public MovieLogic(IRepository<Movie> repo, IRepository<Actors> actorRepo, IRepository<Role> roleRepo,
        IRepository<Network> networkRepo)
        {
            this.repo = repo;
            this.actorRepo = actorRepo;
            this.roleRepo = roleRepo;
            this.networkRepo = networkRepo;

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
            if (movie == null)
            {
                throw new ArgumentException("This movie doesn't exists!");
            }
            return movie;
        }

        public IEnumerable<Movie> ReadAll()
        {
            return this.repo.ReadAll();

        }

        public void Update(Movie item)
        {
            this.repo.Update(item);
        }

        //5 NON CRUD
        //Average episode on movies released by each network
        public IEnumerable<KeyValuePair<string, double>> AvgEpisodesPerNetwork()
        {

            var q1 = from x in repo.ReadAll()
                     group x by x.NetworkId into g
                     select new KeyValuePair<string, double>(networkRepo.Read(g.Key).NetworkName, g.Average(t => t.Episodes));

            return q1;
        }

        //Filmek 10 feletti értékeléssel és ezek főszereplői

      public IEnumerable<KeyValuePair<string, string>> MoviesWith10RatingWithMainActor()
        {
            var q2 = from x in repo.ReadAll()
                     from r in roleRepo.ReadAll()
                     join a in actorRepo.ReadAll() on r.ActorId equals a.ActorId
                     where x.Rating == 10 && r.Priority == 1
                     select new KeyValuePair<string, string>(x.Title, a.ActorName);
 
           return q2;
        }
       // avarage movie rate by network
       public IEnumerable<KeyValuePair<string, double>> AvgMovieRateByNetwork()
        {
            var q3 = from x in repo.ReadAll()
                     group x by x.NetworkId into g
                     select new KeyValuePair<string, double>(networkRepo.Read(g.Key).NetworkName, g.Average(t => t.Rating));

            return q3;
        }

       //average duration of movies by network
       //average duration by actors

        /*public class AvgEpByNetwork
        {
            public string networkName { get; set; }
            public  double? avgEpPerMovie{ get; set; }

            public override bool Equals(object obj)
            {
                AvgEpByNetwork b = obj as AvgEpByNetwork;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.networkName == b.networkName
                        && this.avgEpPerMovie == b.avgEpPerMovie;
                        
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.networkName, this.avgEpPerMovie);
            }
        }*/
       

    }

    
}
