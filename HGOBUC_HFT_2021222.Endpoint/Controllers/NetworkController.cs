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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HGOBUC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        INetworkLogic logic;
        IHubContext<SignalRHub> hub;

        public NetworkController(INetworkLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Network> ReadAll()
        {
            return this.logic.ReadAll();
           
        }

        [HttpGet("{id}")]
        public Network Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Network value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("NetworkCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Network value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("NetworkUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var NedtworkToDelete = logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DetworkDeleted", NedtworkToDelete);
        }
    }
}
