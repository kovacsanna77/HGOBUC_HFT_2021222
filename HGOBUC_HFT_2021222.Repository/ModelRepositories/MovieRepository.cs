using HGOBUC_HFT_2021222.Models;
using HGOBUC_HFT_2021222.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Repository.ModelRepositories
{
    public class MovieRepository : Repository<Movie>, IRepository<Movie>
    {
        public MovieRepository(MovieDbContext ctx): base(ctx)
        {

        }
        public override Movie Read(int id)
        {
            return ctx.Movies.FirstOrDefault(m => m.MovieId == id);
        }

        public override void Update(Movie item)
        {
            var old = Read(item.MovieId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
