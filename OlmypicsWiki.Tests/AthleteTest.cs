using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OlympicsWiki.Controllers;
using OlympicsWiki.DB;
using System;
using System.Linq;
namespace OlmypicsWiki.Tests
{
    [TestClass]
    public class AthleteTest
    {
        AthletesController athletesController;
        AppDBContext db;
        public AthleteTest ()
        {
            db = new OlympicsWiki.DB.AppDBContext();
            athletesController = new AthletesController(db);
        }
        [TestMethod]
        public void SearchNoResult ()
        {
            var response = athletesController.Searches(new AthletesController.AthletesSearch()
            {
                Country = "xxxx",
                Name = "xyxyyaxyx"
            });
            Assert.AreEqual(0, response.athletes.Count);
        }

        [TestMethod]
        public void SearchOneResult ()
        {
            var response = athletesController.Searches(new AthletesController.AthletesSearch()
            {
                Name = "USAIN"
            });
            Assert.AreEqual(1, response.athletes.Count);
        }
        [TestMethod]
        public void SearchWithEmptyFilter ()
        {
            var response = athletesController.Searches(new AthletesController.AthletesSearch()
            {
            });
            Assert.AreEqual(db.Athletes.Count(), response.athletes.Count);
        }

        [TestMethod]
        public void SearchForCountry ()
        {
            var country = db.Athletes.First().Country;
            var response = athletesController.Searches(new AthletesController.AthletesSearch()
            {
                Country = country
            });
            Assert.AreEqual(db.Athletes.Where(x => x.Country == country).ToList().Count, response.athletes.Count);
        }

        [TestMethod]
        public void SearchForBirth ()
        {
            var birth = db.Athletes.First().Birth;
            var response = athletesController.Searches(new AthletesController.AthletesSearch()
            {
                MaxBirth = birth,
                MinBirth = birth
            });
            Assert.AreEqual(db.Athletes.Where(x => x.Birth >= birth && x.Birth <= birth).ToList().Count, response.athletes.Count);
        }
        [TestMethod]
        public void GetAthleteInvalidId ()
        {
            var response = athletesController.Get(-100);
            Assert.IsNull(response);
        }
        [TestMethod]
        public void GetAthlete ()
        {
            var response = athletesController.Get(1);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Birth);
            Assert.IsNotNull(response.BirthPlace);
            Assert.IsNotNull(response.Country);
            Assert.IsNotNull(response.FullName);
            Assert.IsNotNull(response.Id);
            Assert.IsNotNull(response.Sports);
        }
        [TestMethod]
        public void DeleteAthleteInvalidId ()
        {
           athletesController.Delete(-100);//should not throw exception           
        }
        [TestMethod]
        public void SaveNewAthleteUpdateThenDelete ()
        {
            var sport = db.Sports.First();
            //create new
            athletesController.Post(new AthletesController.AthleteDTO()
            {
                Birth = DateTime.Now,
                BirthPlace = "testbirthplace",
                Country = "testcountry",
                FullName = "testname",
                Sports = new System.Collections.Generic.List<AthletesController.SportDTO>() { new AthletesController.SportDTO(){
                Id = sport.Id,
                Name = sport.Name
                }
            }
            });

            var result = db.Athletes.Where(x => x.FullName == "testname").Include(x => x.Sports).ThenInclude(x => x.Sport).SingleOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual("testbirthplace", result.BirthPlace);
            Assert.AreEqual("testcountry", result.Country);
            Assert.AreEqual(sport.Name, result.Sports.First().Sport.Name);
            
            //update existing
            athletesController.Post(new AthletesController.AthleteDTO()
            {
                Id = result.Id,
                Birth = DateTime.Now,
                BirthPlace = "testbirthplacex",
                Country = "testcountryx",
                FullName = "testnamex",
                Sports = new System.Collections.Generic.List<AthletesController.SportDTO>() { new AthletesController.SportDTO(){
                Id = sport.Id,
                Name = sport.Name
                }
            }
            });

            var resultUpdated = db.Athletes.Where(x => x.FullName == "testnamex").Include(x => x.Sports).ThenInclude(x => x.Sport).SingleOrDefault();
            Assert.IsNotNull(resultUpdated);
            Assert.AreEqual("testbirthplacex", resultUpdated.BirthPlace);
            Assert.AreEqual("testcountryx", resultUpdated.Country);
            Assert.AreEqual(sport.Name, resultUpdated.Sports.First().Sport.Name);

            athletesController.Delete(resultUpdated.Id);
            var resultDeleted = db.Athletes.Where(x => x.FullName == "testnamex").Include(x => x.Sports).ThenInclude(x => x.Sport).SingleOrDefault();
            Assert.IsNull(resultDeleted);
        }
    }
}
