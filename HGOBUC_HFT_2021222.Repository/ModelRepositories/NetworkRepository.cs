using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Repository.ModelRepositories
{
    public class NetworkRepository : Repository<Network>, IRepository<Network>
    {
        public NetworkRepository(MovieDbContext ctx) : base(ctx)
        {
                
        }
        public override Network Read(int id)
        {
            return ctx.Networks.FirstOrDefault(n => n.NetworkId == id);
        }

        public override void Update(Network item)
        {
            var old = Read(item.NetworkId);
            foreach (var i in old.GetType().GetProperties())
            {
                i.SetValue(old, i.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
