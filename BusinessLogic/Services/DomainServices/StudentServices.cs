using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Exception;
using BusinessLogic.Resources;
using BusinessLogicInterfaces;
using DataTransferObject;
using EntitiesInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class StudentServices:IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMarkAndVisitedRepository _markAndVisitedRepository;
        private readonly ILogger<StudentServices> _logger;
        private readonly IConfiguration Configuration;
        private readonly int maxMark;
        private readonly int minMark;
        public StudentServices([NotNull]ILogger<StudentServices> logger, 
            [NotNull]IStudentRepository studentRepository, 
            [NotNull]IMarkAndVisitedRepository markAndVisitedRepository,
            [NotNull] IConfiguration configuration)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _markAndVisitedRepository = markAndVisitedRepository;
            Configuration = configuration;
            maxMark = Int32.Parse(Configuration["Constant:MaxMark"]);
            minMark = Int32.Parse(Configuration["Constant:MinMark"]);
        }
        public void Create([NotNull]IUniversityDTO universityDto ,[NotNull]string name)
        {
            IStudentDTO studentDto = new StudentDto
            {
                Name = name,
                UniversityDtoForStudy = universityDto
            };
            _studentRepository.Create(studentDto);
            _logger.LogInformation(ResLoggingText.AddedStudent);
        }
        public List<IStudentDTO> GetAll()
        {
            _logger.LogInformation(ResLoggingText.ReadedStudent);
            return _studentRepository.GetAll();
        }
        public IStudentDTO Get([NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.ReadedStudent);
            return _studentRepository.Get(name);
        }
        public void Update([NotNull]IStudentDTO studentDto, [NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.UpdatedStudent);
            _studentRepository.Update(studentDto, name);
        }
        public void Delete([NotNull]IStudentDTO studentDto)
        {
            _logger.LogInformation(ResLoggingText.DeletedStudent);
            _studentRepository.Delete(studentDto);
        }
        
        public void SetHomeworkMarks([NotNull]IStudentDTO studentDto, [NotNull]IHomeworkDTO homeworkDto, int mark)
        {
            if (mark>maxMark||mark<minMark)
            {
                throw new BusinesseLogicObjectNullException("Student HomeworksMarksInfo doesn't update, Mark isn't correct");
            }
            IStudentHomeworkMarksDTO dto = new StudentHomeworkMarksDto
            {
                StudentDto = studentDto,
                HomeworkDto = homeworkDto,
                Mark = mark
            };
           
            _logger.LogInformation("Set Homeworks marks for Student");
            _markAndVisitedRepository.UpdateHomeworksMark(dto);
        }
         public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByStudent([NotNull]IStudentDTO studentDto)
                {
                    _logger.LogInformation("Ask info for StudentVisitedReport");
                    return _markAndVisitedRepository.GetMarkAndVisitedByStudent(studentDto);
                }
    }
}