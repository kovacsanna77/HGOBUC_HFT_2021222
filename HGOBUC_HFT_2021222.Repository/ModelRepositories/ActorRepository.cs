﻿using HGOBUC_HFT_2021222.Repository.Interface;
using HGOBUC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Repository.ModelRepositories
{
    public class ActorRepository : Repository<Actors>, IRepository<Actors>
    {
        public ActorRepository(MovieDbContext ctx):base(ctx)
        {

        }
        public override Actors Read(int id)
        {
            return ctx.Actors.FirstOrDefault(a => a.ActorId == id);
        }

        public override void Update(Actors item)
        {
            var old = Read(item.ActorId);
            foreach (var i in old.GetType().GetProperties())
            {
                i.SetValue(old, i.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
