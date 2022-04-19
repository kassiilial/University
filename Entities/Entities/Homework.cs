using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Homework
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LectionId { get; set; }
        public Lection Lection { get; set; }
    }
}