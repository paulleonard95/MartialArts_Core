using Martial.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martial.Data.Repositories
{
    class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Badge>  Badges { get; set; }
        public DbSet<User>  Users { get; set; }

        // Configure the context to use sqlite database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseSqlServer("COMPLETE THIS")
                .UseSqlite("Filename=Martial.db");
        }

        // Convenience method to recreate the database thus ensuring the new database takes account of any 
        // changes to the Models or DatabaseContext
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
