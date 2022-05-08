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

        public IQueryable<Movie> ReadAll()
        {
            return this.repo.ReadAll();

        }

        public void Update(Movie item)
        {
            this.repo.Update(item);
        }

        //5 NON CRUD
       
        public IEnumerable<AvgEpByNetwork> AvgEpisodesPerYear()
        {
            var q1 = from m in repo.ReadAll().ToList()
                     join n in networkRepo.ReadAll().ToList() on m.NetworkId equals n.NetworkId
                     group new { m, n } by n.NetworkName into g
                     select new AvgEpByNetwork
                     {
                        networkName = g.Key,
                        avgEpPerMovie = g.Average(x => x.m.Episodes)
                        
                     };

            return q1;

        }

       //Actor with most movies
        public  IEnumerable<Actors> MostMovies()
        {
            var q2 = from x in repo.ReadAll().ToList()
                     from a in actorRepo.ReadAll().ToList()
                     where x.Actors.Contains(a)
                     group a by a into g
                     select new Actors
                     {
                         ActorId = g.Key.ActorId,
                         ActorName = g.Key.ActorName,
                         Movies = g.Key.Movies,
                         Roles = g.Key.Roles
                     };
                


                return q2;
        }


        public class AvgEpByNetwork
        {
            public string networkName { get; set; }
            public double avgEpPerMovie{ get; set; }
        }
       

    }

    
}
