using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataTransferObject;
using Entities.Entities;
using Entities.Repositories.Exception;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        private readonly ILogger<StudentRepository> _logger;
        private readonly ILogger<MarkAndVisitedRepository> _loggerMarkAndVisited;
        private readonly ILogger<LectionRepository> _loggerLection;
        private readonly ILogger<LectorRepository> _loggerLector;
        private readonly ILogger<HomeworkRepository> _loggerHomework;
        private readonly ILogger<UniversityRepository> _loggerUniversity;
        private readonly AppContext _appContext;
        private readonly ILectionRepository _lectionRepository;

        public StudentRepository([NotNull]ILogger<MarkAndVisitedRepository> loggerMarkAndVisited, 
            [NotNull]AppContext appContext,
            [NotNull]ILectionRepository lectionRepository,
            [NotNull]ILogger<UniversityRepository> loggerUniversity,
            [NotNull]ILogger<LectorRepository> loggerLector,
            [NotNull]ILogger<HomeworkRepository> loggerHomework,
            [NotNull]ILogger<StudentRepository> logger,
            [NotNull]ILogger<LectionRepository> loggerLection)
        {
            _logger = logger;
            _loggerMarkAndVisited = loggerMarkAndVisited;
            _appContext = appContext;
            _lectionRepository = lectionRepository;
            _loggerUniversity = loggerUniversity;
            _loggerLector = loggerLector;
            _loggerHomework = loggerHomework;
            _loggerLection = loggerLection;
        }

        public void Create(IStudentDTO studentDto)
        {
            Student student = ToStudent(studentDto);
            CheckConcreteStudentExist(student.Name);
            new UniversityRepository(_loggerUniversity,_appContext).CheckConcreteUniversityDontExist(student.University.Name);
            _appContext.Students.Add(student);
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedStudent);
            new MarkAndVisitedRepository(_appContext, this, _lectionRepository,
                _loggerMarkAndVisited, _logger, _loggerLection, _loggerLector, _loggerHomework, _loggerUniversity).Create(student);
        }
        
        public void Delete([NotNull]IStudentDTO studentDto)
        {
            Student student = ToStudent(studentDto);
            CheckConcreteStudentDontExist(student.Name);
            _appContext
                .Students
                .Remove(_appContext.Students.First(n => n.Name.Equals(student.Name)));
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.DeletedStudent);
        }
        
        public void Update([NotNull]IStudentDTO studentDto, [NotNull]string newName)
        {
            CheckConcreteStudentDontExist(studentDto.Name);
            _appContext
                .Students
                .First(n => n.Name.Equals(studentDto.Name))
                .Name = newName;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdatedStudent);
        }
        
        public List<IStudentDTO> GetAll()
        {
            CheckStudentsExist();
            var AllStudent =  _appContext.Students
                .Select(n => n)
                .Include(n=>n.University)
                .ToList();
            List<IStudentDTO> studentsList = new List<IStudentDTO>();
            foreach (var one in AllStudent)
            {
                studentsList.Add(Get(one.Name));
            }
            _logger.LogInformation(LoggingText.ReadedStudent);
            return studentsList;
        }
        
        public IStudentDTO Get([NotNull]string name)
        {
            CheckConcreteStudentDontExist(name);
            var concreteStudent = _appContext
                .Students
                .Include(n=>n.University)
                .First(n => n.Name.Equals(name));
            IStudentDTO studentDto = new StudentDto
            {
                Name = concreteStudent.Name,
                UniversityDtoForStudy = new UniversityDto
                {
                    Name = concreteStudent.University.Name
                }
            };
            _logger.LogInformation(LoggingText.ReadedStudent);
            return studentDto;
        }
        
        internal void CheckConcreteStudentDontExist(string name)
        {
            CheckStudentsExist();
            if (!_appContext.Students.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemMissingException(ExceptionText.InvalidStudent);
            }
        }
        
        internal void CheckConcreteStudentExist(string name)
        {
            CheckStudentsExist();
            if (_appContext.Students.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemAlreadyExistsException(ExceptionText.StudentExist);
            }
        }
        
        internal void CheckStudentsExist()
        {
            if (!_appContext.Students.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyStudent);
            }
        }
        
        internal static IStudentDTO ToIStudentDTO([NotNull]Student student)
        {
            IStudentDTO studentDto = new StudentDto()
            {
                Name = student.Name,
                UniversityDtoForStudy = UniversityRepository.ToIUniversityDTO(student.University)
            };
            return studentDto;
        }

        internal static Student ToStudent(IStudentDTO studentDto)
        {
            Student student = new Student
            {
                Name = studentDto.Name,
                University = UniversityRepository.ToUniversity(studentDto.UniversityDtoForStudy)
            };
            return student;
        }
    }
}