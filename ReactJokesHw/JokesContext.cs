using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactJokesHw
{
    public class JokesContext : DbContext
    {
        private readonly string _connectionString;

        public JokesContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UserLikedJokes>()
                .HasKey(ulj => new { ulj.UserId, ulj.JokeId });

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.Joke)
                .WithMany(j => j.UserjokeLikes)
                .HasForeignKey(j => j.JokeId);

            modelBuilder.Entity<UserLikedJokes>()
                .HasOne(ulj => ulj.User)
                .WithMany(u => u.UserLikedJokes)
                .HasForeignKey(u => u.UserId);
        }

        public DbSet<Jokes> Jokes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserLikedJokes> UserLikedJokes { get; set; }
    }
}
