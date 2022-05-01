using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Repository.ModelRepositories
{
    public class RoleRepository : Repository<Role>, IRepository<Role>
    {
        public RoleRepository(MovieDbContext ctx) : base(ctx)
        {

        }
        public override Role Read(int id)
        {
            return ctx.Roles.FirstOrDefault(r => r.RoleId == id);
        }

        public override void Update(Role item)
        {
            var old = Read(item.RoleId);
            foreach (var i in old.GetType().GetProperties())
            {
                i.SetValue(old, i.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
