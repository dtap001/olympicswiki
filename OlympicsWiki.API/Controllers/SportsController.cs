using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlympicsWiki.DB;
using static OlympicsWiki.Controllers.AthletesController;

namespace OlympicsWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        AppDBContext dBContext;
        public SportsController (AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IEnumerable<SportDTO> Get ()
        {
            return dBContext.Sports.Where(x => true).Select(x => new SportDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        [HttpPost]
        public  void  Post (SportDTO sport)
        {
            var result = dBContext.Sports.Where(x => x.Name == sport.Name).SingleOrDefault();
            if(result != null)
            {
                return;
            }
            dBContext.Sports.Add(new DB.Models.Sport()
            {
                Name = sport.Name
            });
            dBContext.SaveChanges();
        }
    }
}
