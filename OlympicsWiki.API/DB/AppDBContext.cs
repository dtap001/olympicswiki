using Microsoft.EntityFrameworkCore;
using OlympicsWiki.DB.Models;

namespace OlympicsWiki.DB
{
    public class AppDBContext :DbContext
    {
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<AthleteSport> AthleteSports { get; set; }
      
        public AppDBContext ()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AthleteSport>()
                .HasOne(m => m.Sport)
                .WithMany(xs => xs.Athletes)
                .HasForeignKey(sc => sc.SportId);
            modelBuilder.Entity<AthleteSport>()
               .HasOne(m => m.Athlete)
               .WithMany(xs => xs.Sports)
               .HasForeignKey(sc => sc.AthleteId);   
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath;
            // if (Environment.IsProduction())
            //  {
            //      dbPath = @"Data Source = /db/Backend.db;";
            //   }
            // else
            //  {
            dbPath = @"Data Source = C:\Temp\Backend.db;";
            //  }
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseSqlite(dbPath);
        }

    }
}
