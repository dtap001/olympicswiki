using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlympicsWiki.DB;

namespace OlympicsWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        AppDBContext dBContext;
        public CountryController (AppDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
       
        [HttpGet]
        public IEnumerable<string> Get ()
        {
            return dBContext.Athletes.Select(x => x.Country).Distinct().ToList();
        }

    }
}
