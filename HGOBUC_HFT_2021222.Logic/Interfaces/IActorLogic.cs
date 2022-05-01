using HGOBUC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Interfaces
{
     public interface IActorLogic
    {
        void Create(Actors item);
        Actors Read(int id);
        void Update(Actors item);
        void Delete(int id);
        IQueryable<Actors> ReadAll();
    }
}
