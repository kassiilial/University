using System.Collections.Generic;

namespace DataTransferObject
{
    public interface ILectionDTO
    {
        public string Name { get; set; }
        public ILectorDTO LectorDto { get; set; }
        //public List<IHomework> Homeworks { get; set; }
    }
    public class LectionDto:ILectionDTO
    {
        public string Name { get; set; }
        public ILectorDTO LectorDto { get; set; }
        //public List<IHomework> Homeworks { get; set; } = new List<IHomework>();
        public LectionDto(string name)
        {
            Name = name;
        }

        public LectionDto()
        {
        }
    }
}