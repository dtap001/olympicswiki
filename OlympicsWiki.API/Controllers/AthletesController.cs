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
        [HttpGet]
        public IEnumerable<string> Get ()
        {
            return new string[] { "value1", "value2" };
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
            public int Id { get; set; }
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
            var query = dBContext.Athletes.Include(x => x.Sports).Where(x => true);
            if (!string.IsNullOrEmpty(search.Country))
            {
                query.Where(x => x.Country == search.Country);
            }
            if (search.MaxBirth != null)
            {
                query.Where(x => x.Birth < search.MaxBirth);
            }
            if (search.MinBirth != null)
            {
                query.Where(x => x.Birth > search.MinBirth);
            }
            if (!string.IsNullOrEmpty(search.Name))
            {
                query.Where(x => x.FullName.ToLower().Contains(search.Name.ToLower()));
            }

            query.GroupBy(x => x.Country);

            var result = query.Select(x => new AthleteDTO()
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
        public void Post ([FromBody] string value)
        {
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
        }
    }
}
