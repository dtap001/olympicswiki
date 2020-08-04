using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OlympicsWiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        // GET: api/<AthletesController>
        [HttpGet]
        public IEnumerable<string> Get ()
        {
            return new string[] { "value1", "value2" };
        }


        public class AthletesSearch
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime MaxBirth { get; set; }
            public DateTime MinBirth { get; set; }

        }
        [HttpGet("searches")]
        public string Searches ()
        {
            return "value";
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
