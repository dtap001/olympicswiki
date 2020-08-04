using System;
using System.Collections.Generic;

namespace OlympicsWiki.DB.Models
{
    public class Athlete
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string BirthPlace { get; set; }
        public string Country { get; set; }
        public virtual ICollection<AthleteSport> Sports { get; set; }
    }
}
