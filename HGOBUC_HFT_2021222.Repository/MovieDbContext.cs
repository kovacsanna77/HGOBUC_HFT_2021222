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


            modelBuilder.Entity<Movie>().HasData(new Movie[]
             {
                   new Movie("1#Vincenzo#2020#tvN#20#90#1"),
                   new Movie("2#It's okay to Not Be Okay#2020#16#75#1"),
                   new Movie("3#Save me#2017#16#60#6")
             });

            modelBuilder.Entity<Actors>().HasData(new Actors[]
                        {
                new Actors("1#Song Joong Gi"),
                new Actors("2#Ok Taec Yeon"),
                new Actors("3# Seo Ye Ji")
            });


             modelBuilder.Entity<Network>().HasData(new Network[]
              {
                    new Network("1#tvN"),
                    new Network("2#jTBC"),
                    new Network("3#KBS"),
                    new Network("4#SBS"),
                    new Network("5#MBC"),
                    new Network("6#Netflix")
               });

        }
    }
}
