using HGOBUC_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;


namespace HGOBUC_HFT_2021222.Repository
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Network> Networks { get; set; }

        public MovieDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                   .UseLazyLoadingProxies()
                   .UseInMemoryDatabase("movie");
                ;
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(movie => movie
            .HasOne(movie => movie.Network)
            .WithMany(network => network.Movies)
            .HasForeignKey(movie => movie.NetworkId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Actors>()
                .HasMany(x => x.Movies)
                .WithMany(x => x.Actors);
                
        }
    }
}
