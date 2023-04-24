using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MarksAndVisited> MarksAndVisiteds { get; set; } = new List<MarksAndVisited>();
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}