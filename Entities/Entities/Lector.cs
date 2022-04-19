using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Lector
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
        public List<Lection> Lections { get; set; } = new List<Lection>();
    }
}