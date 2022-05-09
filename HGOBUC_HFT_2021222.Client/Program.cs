using ConsoleTools;
using HGOBUC_HFT_2021222.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HGOBUC_HFT_2021222.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Actors")
            {
                Console.WriteLine("Enter Actor Name: ");
                string name = Console.ReadLine();
                rest.Post(new Actors() { ActorName = name }, "actor");
            }
            else if( entity == "Role")
            {
                Console.WriteLine("Enter the role: ");
                string role = Console.ReadLine();
                Console.WriteLine("Id of the movie the role is in: ");
                int movieid = int.Parse(Console.ReadLine());
                Console.WriteLine("Actor playing the role: ");
               int  actorId = int.Parse(Console.ReadLine());
                Console.WriteLine("Priority of the role (int) : ");
                int priority = int.Parse(Console.ReadLine());
                rest.Post(new Role() { RoleName = role, ActorId = actorId, MovieId = movieid, Priority = priority}, "role");

            }else if(entity == "Movie")
            {
                Console.WriteLine("Enter the title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter the number of episodes: ");
                int ep = int.Parse(Console.ReadLine());
                Console.WriteLine("Release year: ");
                int aired = int.Parse(Console.ReadLine());
                Console.WriteLine("Episode duration: ");
                int duration = int.Parse(Console.ReadLine());
                Console.WriteLine("Broadcasting network: ");
                string network = Console.ReadLine();
                Console.WriteLine("Rating: ");
                int rate = int.Parse(Console.ReadLine());
                Console.WriteLine("Roles: ");
                
                rest.Post(new Movie() { Title = title, Episodes = ep, Aired = aired, Duration = duration, Network = new Network {NetworkName = network  }, Rating = rate }, "movie");
            }else if(entity == "Network")
            {
                Console.WriteLine("Enter the network: ");
                string n = Console.ReadLine();
                rest.Post(new Network() { NetworkName = n }, "network");
            }

        }
        static void List(string entity)
        {
            if (entity == "Actors")
            {
                List<Actors> actors = rest.Get<Actors>("actor");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.ActorId + ": " + item.ActorName);
                }
            }
            else if (entity == "Role")
            {
                List<Role> roles = rest.Get<Role>("role");
                foreach (var item in roles)
                {
                    Console.WriteLine(item.RoleId + " : " + item.RoleName);
                }
            }
            else if (entity == "Movie")
            {
                List<Movie> movies = rest.Get<Movie>("movie");
                foreach (var item in movies)
                {
                    Console.WriteLine(item.Title);
                }
            }
            else if (entity == "Network")
            {
                List<Network> networks = rest.Get<Network>("network");
                foreach (var item in networks)
                {
                    Console.WriteLine(item.NetworkName);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Actors")
            {
                Console.Write("Enter Actor's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Actors one = rest.Get<Actors>(id, "actor");
                Console.Write($"New name [old: {one.ActorName}]: ");
                string name = Console.ReadLine();
                one.ActorName = name;
                rest.Put(one, "actor");

            }else if(entity == "Movie")
            {
                Console.WriteLine("Enter the movie's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Movie one = rest.Get<Movie>(id, "movie");
                Console.Write($"New name [old: {one.Title}]: ");
                string title = Console.ReadLine();
                one.Title = title;
                rest.Put(one, "movie");

            }
            else if(entity == "Role")
            {
                Console.WriteLine("Enter the role's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Role one = rest.Get<Role>(id, "role");
                Console.Write($"New name [old: {one.RoleName}]: ");
                string name = Console.ReadLine();
                one.RoleName= name;
                rest.Put(one, "role");
            }
            else if(entity == "Network")
            {
                Console.WriteLine("Enter the network's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Network one = rest.Get<Network>(id, "network");
                Console.Write($"New name [old: {one.NetworkName}]: ");
                string name = Console.ReadLine();
                one.NetworkName = name;
                rest.Put(one, "movie");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Actors")
            {
                Console.Write("Enter Actor's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "actor");
            }else if(entity == "Role")
            {
                Console.Write("Enter Role's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "role");
            }
            else if (entity == "Movie")
            {
                Console.Write("Enter Movie's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "movie");
            }
            else if (entity == "Network")
            {
                Console.Write("Enter the network's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "network");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27826/", "movie");

            var actorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Actors"))
                .Add("Create", () => Create("Actors"))
                .Add("Delete", () => Delete("Actors"))
                .Add("Update", () => Update("Actors"))
                .Add("Exit", ConsoleMenu.Close);

            var roleSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Role"))
                .Add("Create", () => Create("Role"))
                .Add("Delete", () => Delete("Role"))
                .Add("Update", () => Update("Role"))
                .Add("Exit", ConsoleMenu.Close);

            var networkSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Network"))
                .Add("Create", () => Create("Network"))
                .Add("Delete", () => Delete("Network"))
                .Add("Update", () => Update("Network"))
                .Add("Exit", ConsoleMenu.Close);

            var movieSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Movie"))
                .Add("Create", () => Create("Movie"))
                .Add("Delete", () => Delete("Movie"))
                .Add("Update", () => Update("Movie"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Movies", () => movieSubMenu.Show())
                .Add("Actors", () => actorSubMenu.Show())
                .Add("Roles", () => roleSubMenu.Show())
                .Add("Networks", () => networkSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
