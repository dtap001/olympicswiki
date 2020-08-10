using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlympicsWiki.DB;
using OlympicsWiki.DB.Models;

namespace OlympicsWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        AppDBContext dBContext;
        public AthletesController (AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        // GET: api/<AthletesController>
        [HttpGet("{id}")]
        public AthleteDTO Get (int id)
        {
            return dBContext.Athletes.Where(x => x.Id == id).Select(x => new AthleteDTO()
            {
                Id = x.Id,
                Birth = x.Birth,
                BirthPlace = x.BirthPlace,
                Country = x.Country,
                FullName = x.FullName,
                Sports = x.Sports.Select(y => new SportDTO()
                {
                    Id = y.SportId,
                    Name = y.Sport.Name
                }).ToList()
            }).SingleOrDefault();
        }

        public class AthletesSearch
        {
            public string Name { get; set; }
            public DateTime? MaxBirth { get; set; }
            public DateTime? MinBirth { get; set; }

            public string Country { get; set; }
        }
        public class AthletesSearchResponse
        {
            public List<AthleteDTO> athletes { get; set; }
        }
        public class AthleteDTO
        {
            public int? Id { get; set; }
            public string FullName { get; set; }
            public DateTime Birth { get; set; }
            public string BirthPlace { get; set; }
            public string Country { get; set; }
            public List<SportDTO> Sports { get; set; }
        }
        public class SportDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [HttpPost("search")]
        public AthletesSearchResponse Searches (AthletesSearch search)
        {
            var query = dBContext.Athletes.Include(x => x.Sports).ThenInclude(x => x.Sport);
            if (!string.IsNullOrEmpty(search.Country))
            {
                query = query.Where(x => x.Country == search.Country).Include(x => x.Sports).ThenInclude(x => x.Sport);
            }
            if (search.MaxBirth != null)
            {
                query = query.Where(x => x.Birth <= search.MaxBirth).Include(x => x.Sports).ThenInclude(x => x.Sport);
            }
            if (search.MinBirth != null)
            {
                query = query.Where(x => x.Birth >= search.MinBirth).Include(x => x.Sports).ThenInclude(x => x.Sport);
            }
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.FullName.ToLower().Contains(search.Name.ToLower())).Include(x => x.Sports).ThenInclude(x => x.Sport);
            }
            query = query.OrderBy(x => x.Country).Include(x => x.Sports).ThenInclude(x => x.Sport);

            var result = query.ToList().Select(x => new AthleteDTO()
            {
                Birth = x.Birth,
                BirthPlace = x.BirthPlace,
                Country = x.Country,
                FullName = x.FullName,
                Id = x.Id,
                Sports = x.Sports.Select(y => new SportDTO()
                {
                    Id = y.SportId,
                    Name = y.Sport.Name
                }).ToList()
            });
            return new AthletesSearchResponse() { athletes = result.ToList() };
        }


        // POST api/<AthletesController>
        [HttpPost]
        public void Post (AthleteDTO athlete)
        {
            Athlete athleteToSave = dBContext.Athletes.Where(x => x.Id == athlete.Id).SingleOrDefault();
            bool isNew = false;
            if (athleteToSave == null)
            {
                isNew = true;
                athleteToSave = new Athlete();
            }
            athleteToSave.Country = athlete.Country;
            athleteToSave.Birth = athlete.Birth;
            athleteToSave.BirthPlace = athlete.BirthPlace;
            athleteToSave.FullName = athlete.FullName;

            if (!isNew)//remove existing links
            {
                dBContext.AthleteSports.RemoveRange(dBContext.AthleteSports.Where(x => x.AthleteId == athleteToSave.Id));
            }

            if (isNew)
            {
                dBContext.Athletes.Add(athleteToSave);
            }

            dBContext.SaveChanges();
            if (athlete.Sports != null)
            {
                foreach (var sportDTO in athlete.Sports)
                {
                    dBContext.AthleteSports.Add(new AthleteSport()
                    {
                        AthleteId = athleteToSave.Id,
                        SportId = sportDTO.Id
                    });
                }
            }
            dBContext.SaveChanges();
        }

        // PUT api/<AthletesController>/5
        [HttpPut("{id}")]
        public void Put (int id, [FromBody] string value)
        {
        }

        // DELETE api/<AthletesController>/5
        [HttpDelete("{id}")]
        public void Delete (int id)
        {
            var toDelete = dBContext.Athletes.Where(x => x.Id == id).SingleOrDefault();
            if (toDelete == null)
            {
                return;
            }
            dBContext.Athletes.Remove(toDelete);
            dBContext.SaveChanges();
        }
    }
}
