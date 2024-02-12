using Microsoft.EntityFrameworkCore;
using System;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        // DbContext is a class that serves as the main entry point for interacting with a
        // database in Entity Framework Core. It represents a session with the
        // underlying database and provides a way to query and manipulate data
        public DbSet<Move> Move { get; set; }

        //opdr 4 toegevoegd
        DbSet<MoveRating> MoveRating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BornToMove;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(builder);
        }
    }
}
