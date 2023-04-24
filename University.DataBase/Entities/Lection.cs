using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Lection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LectorId { get; set; }
        public Lector Lector { get; set; }
    }
}