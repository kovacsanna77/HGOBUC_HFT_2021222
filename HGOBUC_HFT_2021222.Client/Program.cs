using HGOBUC_HFT_2021222.Repository;
using System;

namespace HGOBUC_HFT_2021222.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieDbContext ctx = new MovieDbContext();

           foreach(var item in ctx.Movies)
            {
                Console.WriteLine(item.Title);
                foreach(var role in item.Roles)
                {
                    Console.WriteLine("\t"+ role.RoleName + " : " + role.Actor.ActorName);
                }
            }
        }
    }
}
