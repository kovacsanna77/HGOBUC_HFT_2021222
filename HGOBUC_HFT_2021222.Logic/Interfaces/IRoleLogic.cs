using HGOBUC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Interfaces
{
    public interface IRoleLogic
    {
        void Create(Role item);
        Role Read(int id);
        void Update(Role item);
        void Delete(int id);
        IQueryable<Role> ReadAll();

    }
}
