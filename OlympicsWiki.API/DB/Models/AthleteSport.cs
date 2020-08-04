using System;

namespace OlympicsWiki.DB.Models
{
    public class AthleteSport
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public Athlete Athlete { get; set; }
        public int SportId{get;set;}
        public Sport Sport { get; set; }
    }
}
