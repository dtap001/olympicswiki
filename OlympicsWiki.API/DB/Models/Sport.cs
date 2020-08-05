using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicsWiki.DB.Models
{
    [Table("Sports")]
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AthleteSport> Athletes { get; set; }
    }
}
