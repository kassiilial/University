using System.Collections.Generic;
using DataTransferObject;

namespace University.BusinessLogic.Services.ReportServices.DifferentReportTypes
{
    public class StudentReport
    {
        public StudentReport()
        {
        }
        public string Student { get; set; }
        public List<string> VisitedLection{ get; set; } = new List<string>();
        public List<string> MissingLection { get; set; } = new List<string>();
        public void MakeReport(List<IStudentLectionVisitedDTO> studentLectionVisitedDtos)
        {
            Student = new string(studentLectionVisitedDtos[0].StudentDto.Name);
            LectionDto lectionDto = new LectionDto();
            foreach (var one in studentLectionVisitedDtos)
            {
                if (one.isVisited)
                {
                    VisitedLection.Add(one.LectionDto.Name);
                }
                else
                {
                    MissingLection.Add(one.LectionDto.Name);
                }
            }
        }
    }
}