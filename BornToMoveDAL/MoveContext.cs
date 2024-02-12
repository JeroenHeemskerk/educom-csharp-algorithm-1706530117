using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BornToMove.DAL;

namespace BornToMove.DAL
{
    public class MoveContext : DbContext
    {
        // DbContext is a class that serves as the main entry point for interacting with a
        // database in Entity Framework Core. It represents a session with the
        // underlying database and provides a way to query and manipulate data
        public DbSet<Move> Move { get; set; }
        DbSet<MoveRating> MoveRating { get; set; }


        public void CheckAndAddMovesIfTableEmpty()
        {
            if (!Move.Any())
            {
                AddMoves();
                SaveChanges();
                Console.WriteLine("Moves added to the database.");
            }
            else
            {
                Console.WriteLine("The 'move' table is not empty. No action needed.");
            }
        }

        private void AddMoves()
        {
            Move.AddRange(
                new Move("Push up","Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes", 3 ),
                new Move ("Planking","Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast", 3),
                new Move ("Squat", "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes", 5 )
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BornToMove;Trusted_Connection=true;TrustServerCertificate=true;");
            base.OnConfiguring(builder);
        }
    }
}
