using System.Collections.Generic;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface IAssessmentServices
    {
        public void RecordAttendance(ILectorDTO lectorDto, ILectionDTO lectionDto, List<IStudentDTO> allStudent,
            List<IStudentDTO> studentsVisited);
        public void MarkStudent(IStudentDTO studentDto, IHomeworkDTO homeworkDto, int mark);
        
    }
   
}