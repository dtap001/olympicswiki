using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlympicsWiki.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace OlympicsWiki.Tests
{
    [TestClass]
    public class DataFiller
    {
        [TestMethod]
        public void GenerateBasicData ()
        {
            var db = new AppDBContext();
            db.Database.EnsureCreated();

            var running = new DB.Models.Sport()
            {
                Name = "Running"
            };
            AddSportIfNotExists(db, running);

            var jumping = new DB.Models.Sport()
            {
                Name = "Jumping"
            };
            AddSportIfNotExists(db, jumping);

            var swimming = new DB.Models.Sport()
            {
                Name = "Swimming"
            };
            AddSportIfNotExists(db, swimming);

            var box = new DB.Models.Sport()
            {
                Name = "Boxing"
            };
            AddSportIfNotExists(db, box);

            var usainBolt = new DB.Models.Athlete()
            {
                FullName = "Usain Bolt",
                Birth = DateTime.Parse("1986.08.21."),
                BirthPlace = "Jamaica",
                Country = "Jamaica",
            };
            AddAthleteIfNotExists(db, usainBolt);
            ConnectAthleteToSportIfNotExists(db, usainBolt, running);

            var mPhelps = new DB.Models.Athlete()
            {
                FullName = "Michael Phelps",
                Birth = DateTime.Parse("1985.06.30."),
                BirthPlace = "USA",
                Country = "USA",
            };
            AddAthleteIfNotExists(db, mPhelps);
            ConnectAthleteToSportIfNotExists(db, mPhelps, swimming);

            var mAli = new DB.Models.Athlete()
            {
                FullName = "Muhammad Ali",
                Birth = DateTime.Parse("1942.01.17."),
                BirthPlace = "USA",
                Country = "USA",
            };
            AddAthleteIfNotExists(db, mAli);
            ConnectAthleteToSportIfNotExists(db, mAli, box);

            var pMorales = new DB.Models.Athlete()
            {
                FullName = "Pablo Morales",
                Birth = DateTime.Parse("1964.12.05."),
                BirthPlace = "USA",
                Country = "USA",
            };
            AddAthleteIfNotExists(db, pMorales);
            ConnectAthleteToSportIfNotExists(db, pMorales, swimming);

        }

        private void AddSportIfNotExists (AppDBContext db, DB.Models.Sport sport)
        {
            if (db.Sports.Where(s => s.Name == sport.Name).SingleOrDefault() != null)
            {
                return;
            }
            db.Sports.Add(sport);
            db.SaveChanges();
        }

        private void AddAthleteIfNotExists (AppDBContext db, DB.Models.Athlete athlete)
        {
            if (db.Athletes.Where(s => s.FullName == athlete.FullName).SingleOrDefault() != null)
            {
                return;
            }
            db.Athletes.Add(athlete);
            db.SaveChanges();
        }

        private void ConnectAthleteToSportIfNotExists (AppDBContext db, DB.Models.Athlete athlete, DB.Models.Sport sport)
        {
            if (db.AthleteSports.Where(s => s.Athlete.FullName == athlete.FullName && s.Sport.Name == sport.Name).SingleOrDefault() != null)
            {
                return;
            }
            db.AthleteSports.Add(new DB.Models.AthleteSport()
            {
                Athlete = athlete,
                Sport = sport
            });
            db.SaveChanges();
        }
    }
}
