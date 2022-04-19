using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Assessment : Controller
    {
        private readonly ILogger<Assessment> _logger;
        private readonly IAssessmentServices _assessmentServices;
        private readonly IStudentServices _studentServices;
        private readonly IHomeworkServices _homeworkServices;
        private readonly ILectionServices _lectionServices;
        private readonly ILectorServices _lectorServices;
        private readonly IVisitReport _visitReport;
        
        public Assessment(ILogger<Assessment> logger, IAssessmentServices assessmentServices, 
            IStudentServices studentServices, IHomeworkServices homeworkServices, ILectionServices lectionServices,
            ILectorServices lectorServices, IVisitReport visitReport)
        {
            _logger = logger;
            _assessmentServices = assessmentServices;
            _studentServices = studentServices;
            _homeworkServices = homeworkServices;
            _lectionServices = lectionServices;
            _lectorServices= lectorServices;
            _visitReport = visitReport;
        }
        
      ///<summary>Выставить оценку студенту домашнюю работу</summary>
      [HttpGet("Student name, Homework name, Mark")]
      public void SetMark(string studentName, string homeworkName, int mark)
        {
            IStudentDTO studentDto = _studentServices.Get(studentName);
            IHomeworkDTO homeworkDto = _homeworkServices.Get(homeworkName);
            _assessmentServices.MarkStudent(studentDto, homeworkDto, mark);
            _logger.LogInformation("Set Mark from WEB");
        }
        
      ///<summary>Отчет по посещаемости для студента в одном из форматов</summary>
        [HttpGet("Student name, Serilisate to")]
        public string StudentReport(string name, MySerializator serilizate)
        {
            return _visitReport.GenerateSerialisedVisitReport(_studentServices.Get(name), serilizate);
        }
        
      ///<summary>Отчет по посещаемости лекции в одном из форматов</summary>
        [HttpGet("Lection name, Serilisate to")]
        public string LectionReport(string name, MySerializator serilizate)
        {
            return _visitReport.GenerateSerialisedVisitReport(_lectionServices.Get(name), serilizate);
        }
        
      ///<summary>Заполнение посещаемости лекции</summary>
        [HttpGet("Lector Name, Lection Name, All Student, Visited Student")]
        public void RecordLectionAttendance(string lectorName, string lectionName, string allStudent, string visitedStudent)
        {
            ILectionDTO lectionDto = _lectionServices.Get(lectionName);
            ILectorDTO lectorDto = _lectorServices.Get(lectorName);
            List<IStudentDTO> allStudentList = Parser(allStudent);
            List<IStudentDTO> visitedStudentList = Parser(visitedStudent);
            _assessmentServices.RecordAttendance(lectorDto, lectionDto, allStudentList, visitedStudentList);
            _logger.LogInformation("There was Lection");
        }
        
        private List<IStudentDTO> Parser(string student)
        {
            List<IStudentDTO> listStudents = new List<IStudentDTO>();
            string[] students = student.Split(',');
            foreach (var onestudent in students)
            {
                listStudents.Add(_studentServices.Get(onestudent.Trim()));
            }
            _logger.LogInformation("Parse student List");
            return listStudents;
        }
    }
}