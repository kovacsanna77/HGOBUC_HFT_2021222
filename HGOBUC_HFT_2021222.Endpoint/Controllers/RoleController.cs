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
    public class RoleController : ControllerBase
    {
        IRoleLogic logic;
        IHubContext<SignalRHub> hub;

        public RoleController(IRoleLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Role> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Role Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Role value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RoleCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Role value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RoleUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var roleTodelete = logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RoleDeleted", roleTodelete);
        }
    }
}
