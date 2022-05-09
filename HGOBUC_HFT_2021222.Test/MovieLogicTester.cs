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
        Mock<IRepository<Movie>> mockMovieRepo;

        [SetUp]
        public void Init()
        {
            mockMovieRepo = new Mock<IRepository<Movie>>();
            mockMovieRepo.Setup(m => m.ReadAll()).Returns(new List<Movie>()
            {
                new Movie("1#MovieA#2000#16#60#1#5"),
                new Movie("2#MovieB#2001#18#70#2#6"),
                new Movie("3#MovieC#2002#10#80#3#7"),
                new Movie("4#MovieD#2003#12#85#4#8"),
                new Movie("5#MovieE#2004#8#60#5#10"),
                new Movie("6#MovieF#2005#12#85#6#9"),
            }.AsQueryable());
            logic = new MovieLogic(mockMovieRepo.Object);
        }

        [Test]
        public void AvgEpisodesPerNetworkTest()
        {
            var actual = logic.AvgEpisodesPerNetwork().ToList();
            var expected = new List<AvgEpByNetwork>
            {
                new AvgEpByNetwork()
                {
                    networkName = "tVN",
                    avgEpPerMovie = 18,
                },
                new AvgEpByNetwork()
                {
                    networkName = "jTBC",
                    avgEpPerMovie = 16
                },
                new AvgEpByNetwork()
                {
                    networkName = "KBS",
                    avgEpPerMovie = 2
                },
                new AvgEpByNetwork()
                {
                    networkName = "SBS",
                    avgEpPerMovie = 0
                },
                new AvgEpByNetwork()
                {
                    networkName = "MBC",
                    avgEpPerMovie = 17
                },
                new AvgEpByNetwork()
                {
                    networkName = "Netflix",
                    avgEpPerMovie = 12
                }
            };

            Assert.AreEqual(expected, actual);
        }
    }
}
