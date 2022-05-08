using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HGOBUC_HFT_2021222.Logic.Classes.MovieLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HGOBUC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IMovieLogic logic;

        public StatController(IMovieLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<AvgEpByNetwork> AvgEpisodesPerYear()
        {
            return this.logic.AvgEpisodesPerNetwork();
        }
        //[HttpGet]
      /*  public IEnumerable<KeyValuePair<string, double>> MostMovies()
        {
            return this.logic.MostMovies();
        }*/
    }
}
