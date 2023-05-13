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
                   new Movie("1#Vincenzo#2020#20#90#1#10"),   //tvN 20 ep 10 rating 90 min 2020
                   new Movie("2#Itaewon Class#2020#16#70#2#9"), //jTBC 16 ep 9 rating 70 min 2020
                   new Movie("3#My name#2021#8#50#6#8"), //netflix 8 ep 8 rating 50 min 2021
                   new Movie("4#Military Prosecutor Doberman#2022#16#60#1#7"),  //tvN 16 ep 60min 7 rating 2022
                   new Movie("5#Arthdal Chronicles#2019#18#80#1#10"), // tvN 18 ep 10 rating 80 min 2019
                   new Movie("6#My Liberation Notes#2022#16#60#6#9"), //netflix 16 ep 9 ratin 60min 2022
                   new Movie("7#Just Between Lovers#2017#16#75#2#10"), //jtbc 16 ep 10 rating 75 min 2017
                   new Movie("8#Red Cuff of the Sleeve#2021#17#80#5#5"), //MBC 17 ep 5 rating 80 min 2021
                   new Movie("9#After the Rain#2018#20#65#3#6"), //KBS 20 ep 6 rating 65 min 2018
                   new Movie("10#Black Knight#2023#6#50#6#4"),//Netflic 6 ep 8 rating 50 min 2022
                   new Movie("11#Figth For My Way#2017#16#60#3#8"),  //KBS 16 ep 8 rating 60 min 2017
                   new Movie("12#Tomorrow#2022#16#60#5#10"), //MBC 16 ep 10 rating 60 min 2022
                   

            });

            modelBuilder.Entity<Actors>().HasData(new Actors[]
            {
                new Actors("1#Song Joong Gi"),
                new Actors("2#Ok Taec Yeon"),
                new Actors("3#Han So Hee"),
                new Actors("4#Ahn Bo Hyun"),
                new Actors("5#Park Seo Joon"),
                new Actors("6#Jeon Yeo Bin"),
                new Actors("7#Kim Ji Won"),
                new Actors("8#Lee Jon Ho"),
                new Actors("9#Won Jin Ah"),
                new Actors("10#Kim Woo Bin"),
                new Actors("11#Ro Woon"),
                new Actors("12#Kim Hee Sun"),
                new Actors("13#Lee Se Young")
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
                new Role("1#1#1#1#Vincenzo Cassano"), //1 Vincenzo SongJoongGi
                new Role("2#1#2#3#Jun Woo"), //2 Vincenzo TaecYeon
                new Role("3#1#6#2#Hong Cha Young"), //2 Vincenzo Jeon Yeo Bin
                new Role("4#3#3#1#Yoon Ji Woo"), //4 My Name han soo hee
                new Role("5#3#4#2#Jeon Pil Do"), // 5 my name ahn bo hyun
                new Role("6#2#5#1#Park Sae Roy"), //6 itaewon class seo joon
                new Role("7#4#4#1#Do Bae Man"), //7 military pros ahn bo hyun
                new Role("8#5#1#1#Eun Som/Sa Ya"), // 8 arthdal chronicles SongJoongGi
                new Role("9#5#7#3#Tan Ya"), // 9 arthdal    kim ji won
                new Role("10#6#7#1#Yeom Mi Jung"), //10 my liberation notes kim ji won
                new Role("11#7#8#1#Lee Gang Doo"), //11 just between lovers lee joon ho
                new Role("12#7#9#1#Han Moon Soo"),//12 just between lovers Won Jin Ah
                new Role("13#8#8#2#Yi San/King Jung Jo"), //13 red cuff Lee Jon Ho
                new Role("14#9#3#4#Soo Jin"), // 14 after the rain  han soo hee
                new Role("15#11#5#1#Ko Dong Man"), // 15 fight for my way park seo joon
                new Role("16#11#7#1#Choi Ae Ra"),//16 fight for my way  kim ji won
                new Role("17#10#10#1#5-8"), // 17 black knight kim woo bin
                new Role("18#12#11#1#Choi Joon Woong"), 
                new Role("19#12#12#1#Goo Ryun"),
                new Role("20#8#13#1#Sung Deok Im"), //20 red cuff  lee se young



            });

        }
    }
}
