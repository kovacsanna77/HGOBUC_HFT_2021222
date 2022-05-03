using HGOBUC_HFT_2021222.Logic.Classes;
using HGOBUC_HFT_2021222.Repository;
using HGOBUC_HFT_2021222.Repository.ModelRepositories;
using System;

namespace HGOBUC_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieDbContext ctx = new MovieDbContext();

            var movieRepo = new MovieRepository(ctx);
            var actorRepo = new ActorRepository(ctx);
            var roleRepo = new RoleRepository(ctx);
            var networkRepo = new NetworkRepository(ctx);

            var movieLogic = new MovieLogic(movieRepo);
            var actorLogic = new ActorLogic(actorRepo);
            var roleLogic = new RoleLogic(roleRepo);
            var networkLogic = new NetworkLogic(networkRepo);
           

        }
    }
}
