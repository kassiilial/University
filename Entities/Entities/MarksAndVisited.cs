using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class MarksAndVisited
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public Homework Homework { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Mark { get; set; }
        public bool IsVisited { get; set; }
    }
}