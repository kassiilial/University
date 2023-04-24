
using System.Collections.Generic;

namespace DataTransferObject
{
    public interface ILectorDTO
    {
        public string Name { get; set; }
        public IUniversityDTO UniversityDtoForWork { get; set; }
        //public List<ILection> AllLections { get; set; }
    }
    public class LectorDto:ILectorDTO
    {
        public string Name { get; set; }
        public IUniversityDTO UniversityDtoForWork { get; set; }
        //public List<ILection> AllLections { get; set; } = new List<ILection>();
        public LectorDto(string name)
        {
            Name = name;
        }

        public LectorDto()
        {
        }
    }
}