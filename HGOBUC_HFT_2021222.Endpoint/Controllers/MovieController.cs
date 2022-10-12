using HGOBUC_HFT_2021222.Endpoint.Services;
using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace HGOBUC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieLogic logic;
        IHubContext<SignalRHub> hub;

        public MovieController(IMovieLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Movie> ReadAll()
        {
            return this.logic.ReadAll();
        }

        
        [HttpGet("{id}")]
        public Movie Read(int id)
        {
            return this.logic.Read(id);
        }

        
        [HttpPost]
        public void Create([FromBody] Movie value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("MovieCreated", value);
        }

        
        [HttpPut]
        public void Update( [FromBody] Movie value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("MovieUpdated", value);
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieToDelete = logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("MoviDeleted", movieToDelete);
        }
    }
}
