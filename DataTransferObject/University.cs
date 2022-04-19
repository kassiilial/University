using System.Collections.Generic;

namespace DataTransferObject
{
    public interface IUniversityDTO
    {
        public string Name { get; set; }
        //public List<IStudent> AllStudents { get; set; }
        //public List<ILector> AllLector { get; set; }
    }
    public class UniversityDto:IUniversityDTO
    {
        public string Name { get; set; }
        //public List<IStudent> AllStudents { get; set; } = new List<IStudent>();
        //public List<ILector> AllLector { get; set; } = new List<ILector>();
        public UniversityDto(string name)
        {
            Name = name;
        }

        public UniversityDto()
        {
        }
    }
}