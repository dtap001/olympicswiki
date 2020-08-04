using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicsWiki.DB.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AthleteSport> Athletes { get; set; }
    }
}
