using HGOBUC_HFT_2021222.Logic.Classes;
using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static HGOBUC_HFT_2021222.Logic.Classes.MovieLogic;

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
               
                
            }.AsQueryable();
            var actor = new List<Actors>()
            {
               
                
            }.AsQueryable();
            var movie = new List<Movie>()
            {
              
                
            }.AsQueryable();

            var network = new List<Network>()
            {
               
                
            }.AsQueryable();

            mockMovieRepo.Setup(m => m.ReadAll()).Returns(movie); 
            mockNetworkRepo.Setup(n => n.ReadAll()).Returns(network);
            mockActorRepo.Setup(a => a.ReadAll()).Returns(actor);
            mockRoleRepo.Setup(r => r.ReadAll()).Returns(role);

            logic = new MovieLogic(mockMovieRepo.Object);
            networkLogic = new NetworkLogic(mockNetworkRepo.Object);
            actorLogic = new ActorLogic(mockActorRepo.Object);
            roleLogic = new RoleLogic(mockRoleRepo.Object);
        }

        [Test]
        public void AvgMovieRateByNetwork()
        {
            var actual = logic.AvgMovieRateByNetwork().ToList();
            

            Assert.AreEqual(actual[1], new KeyValuePair<string, double>("jTBC", 9));
        }
        [Test]
        public void AvgEpisodesPerNetworkTest()
        {
            var actual = logic.AvgEpisodesPerNetwork().ToArray();

            var expected =
                 new KeyValuePair<string, double?>("jTBC", 16);

            Assert.That(actual[1], Is.EqualTo(expected));
         }

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
