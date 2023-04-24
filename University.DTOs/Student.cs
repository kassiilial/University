
using System.Collections.Generic;

namespace DataTransferObject
{
    public interface IStudentDTO
    {
        public string Name { get; set; }
        public IUniversityDTO UniversityDtoForStudy { get; set; }
    }
    public class StudentDto:IStudentDTO
    {
        public string Name { get; set; }
        public IUniversityDTO UniversityDtoForStudy { get; set; }
        
        public override bool Equals(object other)
        {
            if (other==null)
            {
                return false;
            }
            if (!(other is IStudentDTO otherStudent))
            {
                return false;
            }
            if (Name.Equals(otherStudent.Name))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Name, UniversityDtoForStudy.Name).GetHashCode();
        }
    }
    
    
}