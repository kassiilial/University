using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DataTransferObject;

namespace BusinessLogic.Services
{
    public class LectionReport
    {
        public string Lection { get; set; }
        public List<string> VisitedStudent { get; set; } = new List<string>();
        public List<string> MissingStudent { get; set; } = new List<string>();

        public LectionReport()
        {
        }
        
        public void MakeReport([NotNull]List<IStudentLectionVisitedDTO> studentLectionVisitedDtos)
        {
            Lection = new string(studentLectionVisitedDtos[0].LectionDto.Name);
            foreach (var one in studentLectionVisitedDtos)
            {
                if (one.isVisited)
                {
                    VisitedStudent.Add(one.StudentDto.Name);
                }
                if (one.isVisited==false)
                {
                    MissingStudent.Add(one.StudentDto.Name);
                }
            }
        }
    }
}