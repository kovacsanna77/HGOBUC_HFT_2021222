using HGOBUC_HFT_2021222.Logic.Interfaces;
using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Logic.Classes
{
    public class NetworkLogic : INetworkLogic
    {
        IRepository<Network> repo;
        public NetworkLogic(IRepository<Network> r)
        {
            this.repo = r;
        }
        public void Create(Network item)
        {
            if(item.NetworkName == string.Empty)
            {
                throw new ArgumentException("Cannot be an empty string.");

            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Network Read(int id)
        {
            return this.repo.Read(id);
            
        }

        public IEnumerable<Network> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Network item)
        {
            this.repo.Update(item);
        }
    }
}
