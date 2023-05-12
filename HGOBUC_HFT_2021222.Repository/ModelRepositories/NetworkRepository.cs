using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
            Network old = Read(item.NetworkId);
            //foreach (var prop in old.GetType().GetProperties())
            //{
            //    prop.SetValue(old, prop.GetValue(item));
            //}
            //ctx.SaveChanges();
            if (old == null)
            {
                throw new ArgumentException("Network not found.");
            }

            foreach (var prop in old.GetType().GetProperties())
            {
                try
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
                catch (TargetException ex)
                {
                    Console.WriteLine($"Error setting value for property {prop.Name}: {ex.InnerException?.Message}");
                }
            }

        }
    }
}
