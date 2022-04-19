using System.Collections.Generic;
using DataTransferObject;

namespace EntitiesInterfaces
{
    public interface IMarkAndVisitedRepository
    {
        public void UpdateHomeworksMark( IStudentHomeworkMarksDTO dto);

        public double AverageMark(IStudentDTO studentDto, IHomeworkDTO homeworkDto);

        public void UpdateLectionVisit(IStudentLectionVisitedDTO dto);
        
        public List<IStudentLectionVisitedDTO> GenerateListOfVisitedStudentsByLector(ILectorDTO lectorDto);

        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByStudent(IStudentDTO studentDto);

        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedReportByLection(ILectionDTO lectionDto);
    }
}