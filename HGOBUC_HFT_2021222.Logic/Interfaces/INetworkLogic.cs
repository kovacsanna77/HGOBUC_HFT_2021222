﻿using HGOBUC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Interfaces
{
    public interface INetworkLogic
    {
        void Create(Network item);
        Network Read(int id);
        void Update(Network item);
        void Delete(int id);
        IEnumerable<Network> ReadAll();
    }
}
