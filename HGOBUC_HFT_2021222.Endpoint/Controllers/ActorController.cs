using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HGOBUC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        IActorLogic logic;

        public ActorController(IActorLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Actors> ReadAll()
        {
            return this.logic.ReadAll(); ;
        }

        [HttpGet("{id}")]
        public Actors Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Actors value)
        {
            this.logic.Create(value);
        }


        [HttpPut]
        public void Update([FromBody] Actors value)
        {
            this.logic.Update(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
