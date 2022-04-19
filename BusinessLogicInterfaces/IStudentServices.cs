using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface IStudentServices
    {
        public void Create(IUniversityDTO universityDto, string name);
        public List<IStudentDTO> GetAll();
        public IStudentDTO Get(string name);
        public void Update(IStudentDTO studentDto, string name);
        public void Delete(IStudentDTO studentDto);
        public void SetHomeworkMarks([NotNull] IStudentDTO studentDto, [NotNull] IHomeworkDTO homeworkDto, int mark);
        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByStudent(IStudentDTO studentDto);
    }
}