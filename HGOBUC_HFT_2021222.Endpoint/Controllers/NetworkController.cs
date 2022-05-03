﻿using HGOBUC_HFT_2021222.Logic.Interfaces;
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
    public class NetworkController : ControllerBase
    {
        INetworkLogic logic;

        public NetworkController(INetworkLogic logic)
        {
            this.logic = logic;
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
        }


        [HttpPut]
        public void Update([FromBody] Network value)
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
