using HGOBUC_HFT_2021222.Logic.Classes;
using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HGOBUC_HFT_2021222.Test
{
    [TestFixture]
    public class MovieLogicTester
    {
        MovieLogic logic;
        NetworkLogic networkLogic;
        ActorLogic actorLogic;
        RoleLogic roleLogic;

        Mock<IRepository<Movie>> mockMovieRepo;
        Mock<IRepository<Network>> mockNetworkRepo;
        Mock<IRepository<Role>> mockRoleRepo;
        Mock<IRepository<Actors>> mockActorRepo;

        [SetUp]
        public void Init()
        {
            mockMovieRepo = new Mock<IRepository<Movie>>();
            mockNetworkRepo = new Mock<IRepository<Network>>();
            mockActorRepo = new Mock<IRepository<Actors>>();
            mockRoleRepo = new Mock<IRepository<Role>>();

            var role = new List<Role>()
            {
                new Role("1#1#1#1#Vincenzo Cassano"),
                new Role("2#1#2#3#Jun Woo"),
                new Role("3#1#6#2#Hong Cha Young"),
                new Role("4#3#3#1#Yoon Ji Woo"),
                new Role("5#3#4#2#Jeon Pil Do"),
                new Role("6#2#5#1#Park Sae Roy"),
                new Role("7#4#4#1#Do Bae Man"),
                new Role("8#5#1#1#Eun Som/Sa Ya"),
                new Role("9#5#7#3#Tan Ya"),
                new Role("10#6#7#1#Yeom Mi Jung"),
                new Role("11#7#8#1#Lee Gang Doo"),
                new Role("12#7#9#1#Han Moon Soo"),
                new Role("13#8#8#1#Yi San/King Jung Jo"),
                new Role("14#9#3#4#Soo Jin")
            }.AsQueryable();

            var movie = new List<Movie>()
            {
              new Movie("1#Vincenzo#2020#20#90#1#10"),
                   new Movie("2#Itaewon Class#2020#16#70#2#9"),
                   new Movie("3#My name#2021#8#50#6#8"),
                   new Movie("4#Military Prosecutor Doberman#2022#16#60#1#7"),
                   new Movie("5#Arthdal Chronicles#2019#18#80#1#10"),
                   new Movie("6#My Liberation Notes#2022#16#60#6#9"),
                   new Movie("7#Just Between Lovers#2017#16#75#2#10"),
                   new Movie("8#Red Cuff of the Sleeve#2021#17#80#5#5"),
                   new Movie("9#After the Rain#2018#2#65#3#6")

            }.AsQueryable();

            var network = new List<Network>()
            {
                new Network("1#tvN"),
                    new Network("2#jTBC"),
                    new Network("3#KBS"),
                    new Network("4#SBS"),
                    new Network("5#MBC"),
                    new Network("6#Netflix")
            }.AsQueryable();

            var actor = new List<Actors>()
            {
                new Actors("1#Song Joong Gi"),
                new Actors("2#Ok Taec Yeon"),
                new Actors("3#Han So Hee"),
                new Actors("4#Ahn Bo Hyun"),
                new Actors("5#Park Seo Joon"),
                new Actors("6#Jeon Yeo Bin"),
                new Actors("7#Kim Ji Won"),
                new Actors("8#Lee Jon Ho"),
                new Actors("9#Woon Jin Ah")
            }.AsQueryable();



            mockMovieRepo.Setup(m => m.ReadAll()).Returns(movie);
            mockNetworkRepo.Setup(n => n.ReadAll()).Returns(network);
            mockActorRepo.Setup(a => a.ReadAll()).Returns(actor);
            mockRoleRepo.Setup(r => r.ReadAll()).Returns(role);
           
            logic = new MovieLogic(mockMovieRepo.Object, mockActorRepo.Object, mockRoleRepo.Object, mockNetworkRepo.Object);
            networkLogic = new NetworkLogic(mockNetworkRepo.Object);
            actorLogic = new ActorLogic(mockActorRepo.Object);
            roleLogic = new RoleLogic(mockRoleRepo.Object);
        }

        //5 NON-CRUD TEST
        [Test]
        public void AvgMovieRateByNetwork()
        {
            var actual = logic.AvgMovieRateByNetwork().ToList();

            var expected = 
                new KeyValuePair<string, double?>("Netflix", 8.5);

            Assert.That(actual[2], Is.EqualTo(expected));
        }
        [Test]
        public void AvgEpisodesPerNetworkTest()
        {
            var actual = logic.AvgEpisodesPerNetwork().ToList();

            var expected =
                 new KeyValuePair<string, double?>("jTBC", 16);

            Assert.That(actual[1], Is.EqualTo(expected));
        }

        [Test]
        public void MoviesWith10ratingWithMainActorTest()
        {
            var actual = logic.MoviesWith10RatingWithMainActor().ToList();

            var expected = 
                new KeyValuePair<string, string>("Vincenzo", "Song Joong Gi");

            Assert.That(actual[0], Is.EqualTo(expected));
        }

        [Test]
        public void ActorWith5RatedMovieTest()
        {
            var actual = logic.ActorWith5RatedMovie().ToList();

            var expected = 
                new KeyValuePair<string, string>("Lee Jon Ho", "Red Cuff of the Sleeve");

            Assert.That(actual[0], Is.EqualTo(expected));
        }
        [Test]
        public void ActorsWhith10RatingTest()
        {
            var actual = logic.ActorsWith10Rating().ToList();

            var expected ="Song Joong Gi";

            Assert.That(actual[0], Is.EqualTo(expected));
        }
      // 3 CREATE TEST + 2 RANDOM 
        [Test]
        public void CreateMovieTestWrongTitle()
        {
            var m = new Movie() { Title = "aa" };
            try
            {
                logic.Create(m);
            }
            catch { }
            mockMovieRepo.Verify(x => x.Create(m), Times.Never);
        }
        [Test]
        public void CreateMovieTest()
        {
            var m = new Movie() { Title = "NewMovie" };
            try
            {
                logic.Create(m);
            }
            catch { }
            mockMovieRepo.Verify(x => x.Create(m), Times.Once);
        }
        [Test]
        public void ActorCreateTest()
        {
            var a = new Actors() { ActorName = "NewActor" };
            try { actorLogic.Create(a); }
            catch { }

            mockActorRepo.Verify(r => r.Create(a), Times.Once);
        }
        [Test]
        public void NetwokrCreateTest()
        {
            var n = new Network() { NetworkName = "" };
            try { networkLogic.Create(n); }
            catch { }

            mockNetworkRepo.Verify(r => r.Create(n), Times.Never);
        }
        [Test]
        public void DeleteRoleTest()
        {
            try { roleLogic.Delete(3); } catch { }
            mockRoleRepo.Verify(r => r.Delete(3), Times.Once);
        }
    }
}
