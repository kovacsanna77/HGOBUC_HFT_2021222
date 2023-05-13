using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HGOBUC_HFT_2021222.Logic.Classes
{
    public class MovieLogic : IMovieLogic
    {
        IRepository<Movie> repo;
        IRepository<Actors> actorRepo;
        IRepository<Role> roleRepo;
        IRepository<Network> networkRepo;
        List<string> nonCruds { get; set; }

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
            if (item.Title.Length < 3)
            {
                throw new ArgumentException("title too short...");
            }
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

        public List<string> getListofNonCruds()
        {
            nonCruds = new List<string>();
            nonCruds.Add("Average episodes per network");
            nonCruds.Add("Movies with 10 ratin and with main actor");
            nonCruds.Add("Average movie ratings by network");
            nonCruds.Add("Actors with 5 rated movies");
            nonCruds.Add("Actros with 10 ratings");
            return nonCruds;
        }
        
        //5 NON CRUD
        //Average episode on movies released by each network
        public IEnumerable<KeyValuePair<string, double?>> AvgEpisodesPerNetwork()
        {

            var q1 = from x in repo.ReadAll()
                     join n in networkRepo.ReadAll() on x.NetworkId equals n.NetworkId
                     group x by n.NetworkName into g
                     select new KeyValuePair<string, double?>(g.Key, g.Average(t => t.Episodes));

            return q1;
        }
        
       //Filmek 10 feletti értékeléssel és ezek főszereplői

      public IEnumerable<KeyValuePair<string, string>> MoviesWith10RatingWithMainActor()
        {
            var q2 = from x in repo.ReadAll()
                     join r in roleRepo.ReadAll() on x.MovieId equals r.RoleId
                     join a in actorRepo.ReadAll() on r.ActorId equals a.ActorId
                     where x.Rating == 10
                     && r.Priority==1
                     select new KeyValuePair<string, string>(x.Title, a.ActorName);
 
           return q2;
        }
       // A csatorna filmejeinek átalgos értékelése
       public IEnumerable<KeyValuePair<string, double?>> AvgMovieRateByNetwork()
        {
            var q3 = from x in repo.ReadAll()
                     join n in networkRepo.ReadAll() on x.NetworkId equals n.NetworkId
                     group x by n.NetworkName into g
                     select new KeyValuePair<string, double?>(g.Key, g.Average(t => t.Rating));
           
            
            return q3;
          
        }

       //A színész által játszott filmek száma 8-nál kisebb értékeléssel
        public IEnumerable<KeyValuePair<string, string>> ActorWith5RatedMovie()
        {
            var q4 = from x in repo.ReadAll()
                     join r in roleRepo.ReadAll() on x.MovieId equals r.MovieId
                     join a in actorRepo.ReadAll() on r.ActorId equals a.ActorId
                     where x.Rating == 5
                     select new KeyValuePair<string, string>(a.ActorName, x.Title);
            return q4;
        }

        //5

      public IEnumerable<string> ActorsWith10Rating()
        {

            var q5 = from x in repo.ReadAll()
                     join r in roleRepo.ReadAll() on x.MovieId equals r.MovieId
                     join a in actorRepo.ReadAll() on r.ActorId equals a.ActorId
                     where x.Rating == 10
                     
                     select a.ActorName;

            return q5;
        }
        
       

    }

    
}
