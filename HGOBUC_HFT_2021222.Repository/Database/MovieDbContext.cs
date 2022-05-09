using HGOBUC_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;


namespace HGOBUC_HFT_2021222.Repository
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Role> Roles { get; set; }

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
                .WithMany(x => x.Actors)
                .UsingEntity<Role>(
                    x => x.HasOne(x => x.Movie)
                        .WithMany().HasForeignKey(x => x.MovieId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Actor)
                        .WithMany().HasForeignKey(x => x.ActorId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Actor)
                .WithMany(actor => actor.Roles)
                .HasForeignKey(r => r.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasOne(r => r.Movie)
                .WithMany(movie => movie.Roles)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Movie>().HasData(new Movie[]
            {
                //ID#Title#Aired#Episodes#Duration#Networkid#Rating
                   new Movie("1#Vincenzo#2020#20#90#1#10"),   
                   new Movie("2#Itaewon Class#2020#16#70#2#9"),
                   new Movie("3#My name#2021#8#50#6#8"),
                   new Movie("4#Military Prosecutor Doberman#2022#16#60#1#7"),
                   new Movie("5#Arthdal Chronicles#2019#18#80#1#8"),
                   new Movie("6#My Liberation Notes#2022#16#60#6#10"),
                   new Movie("7#Just Between Lovers#2017#16#75#2#9"),
                   new Movie("8#Red Cuff of the Sleeve#2021#17#80#5#5"),
                   new Movie("9#After the Rain#2018#2#65#3#6"),
                   
            });

            modelBuilder.Entity<Actors>().HasData(new Actors[]
            {
                new Actors("1#Song Jong Gi"),
                new Actors("2#Ok Taec Yeon"),
                new Actors("3#Han So Hee"),
                new Actors("4#Ahn Bo Hyun"),
                new Actors("5#Park Seo Joon"),
                new Actors("6#Jeon Yeo Bin"),
                new Actors("7#Kim Ji Won"),
                new Actors("8#Lee Jon Ho"),
                new Actors("9#Woon Jin Ah")
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

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                //RoleID#MovieID#ActorId#priority#name
                new Role("1#1#1#1#Vincenzo Cassano"),
                new Role("2#1#2#3#Jun Woo"),
                new Role("3#1#6#2#Hong Cha Young"),
                new Role("4#3#3#1#Yoon Ji Woo"),
                new Role("5#3#4#2#Jeon Pil Do"),
                new Role("6#2#5#1#Park Sae Roy"),
                new Role("7#4#4#1#Do Bae Man"),
                new Role("8#5#1#1#Eun Som/Sa Ya"),
                new Role("9#5#7#3#Tan Ya"),
                new Role("10#6#7#1#Yeom Mi Jung"),
                new Role("11#7#8#1#Lee Gang Doo"),
                new Role("12#7#9#1#Han Moon Soo"),
                new Role("13#8#8#1#Yi San/King Jung Jo"),
                new Role("14#9#3#4#Soo Jin")

            });

        }
    }
}
