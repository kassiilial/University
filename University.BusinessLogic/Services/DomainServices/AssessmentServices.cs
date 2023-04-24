using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using BusinessLogicInterfaces;
using DataTransferObject;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("University.UnitTest")]

namespace University.BusinessLogic.Services.DomainServices
{
    public class AssessmentServices:IAssessmentServices
    { 
        private readonly ILogger<AssessmentServices> _logger;
        private readonly IStudentServices _studentServices;
        private readonly IHomeworkServices _homeworkServices;
        private readonly IMarkAndVisitedRepository _markAndVisitedRepository;
        private readonly IConfiguration Configuration;
        private readonly INotification _notificationEmail;
        private readonly INotification _notificationSms;
        private readonly double _marksForNotification;
        private readonly int _numberLectionForInformation;
        
        public AssessmentServices([NotNull]ILogger<AssessmentServices> logger,  
            [NotNull]IHomeworkServices homeworkServices,
            IConfiguration configuration, [NotNull]IEnumerable<INotification> notifications, [NotNull]IMarkAndVisitedRepository markAndVisitedRepository,
            [NotNull]IStudentServices studentServices)
        {
            _logger = logger;
            _homeworkServices = homeworkServices;
            _studentServices = studentServices;
            Configuration = configuration;
            _markAndVisitedRepository = markAndVisitedRepository;
            _notificationSms = notifications.SingleOrDefault(n=>n.NotificationType.Equals("SMS"));
            _notificationEmail = notifications.SingleOrDefault(n=>n.NotificationType.Equals("Email"));
            _numberLectionForInformation = Int32.Parse(Configuration["Constant:NumberMissingLection"]);
            _marksForNotification = Int64.Parse(Configuration["Constant:MarksForNotification"]);
        }
        
        public void RecordAttendance([NotNull]ILectorDTO lectorDto, [NotNull]ILectionDTO lectionDto, [NotNull]List<IStudentDTO> allStudent, [NotNull]List<IStudentDTO> studentsVisited)
        {
            foreach (var student in allStudent)
            {
                if (studentsVisited.Contains(student))
                {
                    SetLectionIsVisited(lectionDto, student, true);
                }
                else
                {
                    SetLectionIsVisited(lectionDto, student, false);
                    IHomeworkDTO homeworkDto = _homeworkServices.GetByLection(lectionDto);
                    MarkStudent(student, homeworkDto,0);
                }
            }
            List<IStudentDTO> studentWithSkip = GenerateListOfCriticalMissingStudents(lectorDto);
            NotificationAboutCriticalMissingLection(studentWithSkip, lectionDto);
            _logger.LogInformation("There was Lection");
        }

        internal void NotificationAboutCriticalMissingLection(List<IStudentDTO> studentsWith3Out,[NotNull] ILectionDTO lectionDto)
        {
            
            foreach (var student in studentsWith3Out)
            {
                _notificationEmail.Notification(student.Name, $"Вами было пропущено {_numberLectionForInformation} лекции");
                _notificationEmail.Notification(lectionDto.Name, $"Студентом {student.Name} было пропущено {_numberLectionForInformation} лекции");
            }
        }

        public void MarkStudent([NotNull]IStudentDTO studentDto, [NotNull]IHomeworkDTO homeworkDto, int mark)
        {
            _studentServices.SetHomeworkMarks(studentDto, homeworkDto, mark);
            if (AverageMark(studentDto, homeworkDto)<_marksForNotification)
            {
                _notificationSms.Notification(studentDto.Name, $"Ваша средняя оценка опустилась ниже {_marksForNotification} баллов");
                _logger.LogInformation("SMS");   
            }
            _logger.LogInformation("Make a student marked");
        }
        
        internal double AverageMark([NotNull]IStudentDTO studentDto, [NotNull] IHomeworkDTO homeworkDto)
        {
            _logger.LogInformation("Calculate average marks");
            return _markAndVisitedRepository.AverageMark(studentDto, homeworkDto);
        }
        
        internal void SetLectionIsVisited([NotNull]ILectionDTO lectionDto, [NotNull]IStudentDTO studentDto, bool isVisit)
        {
            IStudentLectionVisitedDTO dto = new StudentLectionVisitedDto
            {
                StudentDto = studentDto,
                LectionDto = lectionDto,
                isVisited = isVisit
            };
            _markAndVisitedRepository.UpdateLectionVisit(dto);
        }
        
        internal List<IStudentDTO> GenerateListOfCriticalMissingStudents([NotNull]ILectorDTO lectorDto)
        {
           
            var studentLectionVisited = GetListOfVisitedStudentsByLector(lectorDto);
            
            var info = CalculateStudentMissingByLector(studentLectionVisited);
            
            List<IStudentDTO> studentsWithSkip = new List<IStudentDTO>();
            foreach (var one in info)
            {
                if (one.Visit==_numberLectionForInformation)
                {
                    IStudentDTO studentDto = _studentServices.Get(one.Students);
                    studentsWithSkip.Add(studentDto);
                }
            }
            _logger.LogInformation($"Search student who skip {_numberLectionForInformation} Lection");
            return studentsWithSkip;
        }

        internal dynamic CalculateStudentMissingByLector(List<IStudentLectionVisitedDTO> studentLectionVisited)
        {
            var info = studentLectionVisited
                .Select(n =>new
                {
                    Student = n.StudentDto,
                    IsMissing = n.isVisited
                })
                .GroupBy(s=> s.Student.Name)
                .Select(n=>new
                {
                    Students = n.Key,
                    Visit = n.TakeLast(_numberLectionForInformation).Count(p=>p.IsMissing==false)
                })
                .ToList();
            return info;
        }

        internal List<IStudentLectionVisitedDTO> GetListOfVisitedStudentsByLector([NotNull] ILectorDTO lectorDto)
        {
            return _markAndVisitedRepository.GenerateListOfVisitedStudentsByLector(lectorDto);
        }
    }
}