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
    public class MarkAndVisitedRepository : IMarkAndVisitedRepository
    {
        private readonly ILogger<MarkAndVisitedRepository> _logger;
        private readonly ILogger<StudentRepository> _loggerStudent;
        private readonly ILogger<LectionRepository> _loggerLection;
        private readonly ILogger<LectorRepository> _loggerLector;
        private readonly ILogger<HomeworkRepository> _loggerHomework;
        private readonly ILogger<UniversityRepository> _loggerUniversity;
        private readonly AppContext _appContext;
        private readonly IStudentRepository _studentRepository;
        private readonly ILectionRepository _lectionRepository;

        public MarkAndVisitedRepository([NotNull] AppContext appContext,
            [NotNull] IStudentRepository studentRepository, 
            [NotNull] ILectionRepository lectionRepository,
            [NotNull] ILogger<MarkAndVisitedRepository> logger,
            [NotNull]ILogger<StudentRepository> loggerStudent,
            [NotNull]ILogger<LectionRepository> loggerLection,
            [NotNull]ILogger<LectorRepository> loggerLector,
            [NotNull]ILogger<HomeworkRepository> loggerHomework,
            [NotNull]ILogger<UniversityRepository> loggerUniversity)
        {
            _logger = logger;
            _loggerHomework = loggerHomework;
            _loggerLection = loggerLection;
            _loggerLector = loggerLector;
            _loggerStudent = loggerStudent;
            _loggerUniversity = loggerUniversity;
            _appContext = appContext;
            _studentRepository = studentRepository;
            _lectionRepository = lectionRepository;
        }

        internal void Create([NotNull] Student student)
        {
            new StudentRepository(_logger, _appContext, 
                _lectionRepository, _loggerUniversity,
                _loggerLector, _loggerHomework, _loggerStudent, _loggerLection).CheckConcreteStudentDontExist(student.Name);
            var homeworksList = _appContext
                .Homeworks
                .Where(n => n.Lection.Lector.University.Name.Equals(student.University.Name))
                .ToList();
            foreach (var homework in homeworksList)
            {
                MarksAndVisited marksAndVisited = new MarksAndVisited
                {
                    Student = _appContext
                        .Students
                        .First(n => n.Name.Equals(student.Name)),
                    Homework = _appContext
                        .Homeworks
                        .First(n => n.Name.Equals(homework.Name))
                };
                if (_appContext.MarksAndVisited.Any(n =>
                    n.Homework.Name.Equals(marksAndVisited.Homework.Name)
                    && n.Student.Name.Equals(marksAndVisited.Student.Name)
                ))
                {
                    continue;
                }
                _appContext.MarksAndVisited.Add(marksAndVisited);
            }
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedMarkAndVisited);
        }

        public void Create([NotNull] Homework homework)
        {
            new HomeworkRepository(_loggerHomework, _appContext,
                _studentRepository,_lectionRepository, _loggerUniversity, _logger, _loggerLection,
                _loggerLector, _loggerStudent).CheckConcreteHomeworkDontExist(homework.Name);
            var studentList = _appContext.Students
                .Where(n => n.University.Name.Equals(homework.Lection.Lector.University.Name))
                .ToList();
            var homeworks = _appContext.Homeworks.Select(m => m).ToList();
            foreach (var student in studentList)
            {
                MarksAndVisited marksAndVisited = new MarksAndVisited
                {
                    Student = _appContext
                        .Students
                        .First(n => n.Name.Equals(student.Name)),
                    Homework = _appContext
                        .Homeworks
                        .First(n => n.Name.Equals(homework.Name))
                };
                if (_appContext.MarksAndVisited.Any(n =>
                    n.Homework.Name.Equals(marksAndVisited.Homework.Name)
                    && n.Student.Name.Equals(marksAndVisited.Student.Name)
                ))
                {
                    continue;
                }
                _appContext.MarksAndVisited.Add(marksAndVisited);
            }
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedMarkAndVisited);
        }

        public void UpdateHomeworksMark([NotNull] IStudentHomeworkMarksDTO dto)
        {
            MarksAndVisited marksAndVisited = Converte(dto);
            CheckConcreteMarkAndVisitedDontExist(marksAndVisited);
            _appContext.MarksAndVisited.First(
                n => n.Student.Name.Equals(dto.StudentDto.Name)
                     && n.Homework.Name.Equals(dto.HomeworkDto.Name)
            ).Mark = dto.Mark;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.SetHomework);
        }

        public double AverageMark(IStudentDTO studentDto, IHomeworkDTO homeworkDto)
        {
            MarksAndVisited marksAndVisited = Converte(studentDto, homeworkDto);
            CheckConcreteMarkAndVisitedDontExist(marksAndVisited);
            _logger.LogInformation(LoggingText.AverageMarks);
            return _appContext.MarksAndVisited
                .Include(n => n.Homework)
                .ThenInclude(n => n.Lection.Lector)
                .Where(n => n.Student.Name.Equals(studentDto.Name))
                .Where(n => n.Homework.Lection.Lector.Name.Equals(homeworkDto.LectionDto.LectorDto.Name))
                .Select(n => n.Mark).Average();
        }

        internal MarksAndVisited Converte(IStudentHomeworkMarksDTO dto)
        {
            return Converte(dto.StudentDto, dto.HomeworkDto);
        }

        internal MarksAndVisited Converte(IStudentDTO studentDto, IHomeworkDTO homeworkDto)
        {
            MarksAndVisited marksAndVisited = new MarksAndVisited
            {
                Student = StudentRepository.ToStudent(studentDto),
                Homework = HomeworkRepository.ToHomework(homeworkDto)
            };
            return marksAndVisited;
        }

        public void UpdateLectionVisit([NotNull] IStudentLectionVisitedDTO dto)
        {
            MarksAndVisited marksAndVisited = Converte(dto);
            CheckConcreteMarkAndVisitedDontExist(marksAndVisited);
            _appContext.MarksAndVisited.First(
                n => n.Homework.Lection.Name.Equals(dto.LectionDto.Name)
                     && n.Student.Name.Equals(dto.StudentDto.Name)
            ).IsVisited = dto.isVisited;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdateMarkAndVisited);
        }
        
        internal MarksAndVisited Converte([NotNull] IStudentLectionVisitedDTO dto)
        {
            Homework homework = _appContext.Homeworks.First(n => n.Lection.Name.Equals(dto.LectionDto.Name));
            MarksAndVisited marksAndVisited = new MarksAndVisited
            {
                Student = StudentRepository.ToStudent(dto.StudentDto),
                Homework = homework
                
            };
            return marksAndVisited;
        }
        
        public List<IStudentLectionVisitedDTO> GenerateListOfVisitedStudentsByLector([NotNull] ILectorDTO lectorDto)
        {
            new LectorRepository(_loggerLector, _appContext, _loggerUniversity).CheckConcreteLectorDontExist(LectorRepository.ToLector(lectorDto).Name);
            CheckMarkAndVisitedExist();
            List<IStudentLectionVisitedDTO> forLogic = new List<IStudentLectionVisitedDTO>();
            var info = _appContext.MarksAndVisited
                .Where(n => n.Homework.Lection.Lector.Name.Equals(lectorDto.Name))
                .Select(n =>
                    new 
                    {
                        Student = n.Student,
                        Lection = n.Homework.Lection,
                        IsVisited = n.IsVisited
                    }).ToList(); //TODO: It WAS MISTAKE
            foreach (var one in info)
            {
                IStudentLectionVisitedDTO dto = new StudentLectionVisitedDto
                {
                    StudentDto = _studentRepository.Get(one.Student.Name),
                    LectionDto = _lectionRepository.Get(one.Lection.Name),
                    isVisited = one.IsVisited
                };
                forLogic.Add(dto);
            }
            _logger.LogInformation("Check VisitedInfo in Database");
            return forLogic;
        }
        
        internal void CheckConcreteMarkAndVisitedDontExist(MarksAndVisited marksAndVisited)
        {
            CheckMarkAndVisitedExist();
            if (!_appContext.MarksAndVisited.Any(n =>
                n.Homework.Name.Equals(marksAndVisited.Homework.Name)
                && n.Student.Name.Equals(marksAndVisited.Student.Name)
            ))
            {
                throw new ItemMissingException(ExceptionText.InvalidStudent);
            }
        }

        internal void CheckConcreteMarkAndVisitedExist(MarksAndVisited marksAndVisited)
        {
            CheckMarkAndVisitedExist();
            if (_appContext.MarksAndVisited.Any(n =>
                n.Homework.Name.Equals(marksAndVisited.Homework.Name)
                && n.Student.Name.Equals(marksAndVisited.Student.Name)
            ))
            {
                throw new ItemAlreadyExistsException(ExceptionText.StudentExist);
            }
        }

        internal void CheckMarkAndVisitedExist()
        {
            if (!_appContext.MarksAndVisited.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyStudent);
            }
        }
        
        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByStudent([NotNull] IStudentDTO studentDto)
        {
            new StudentRepository(_logger, _appContext,
                _lectionRepository, _loggerUniversity,
                _loggerLector, _loggerHomework,
                _loggerStudent, _loggerLection).CheckConcreteStudentDontExist(StudentRepository.ToStudent(studentDto).Name);
            IEnumerable<IStudentLectionVisitedDTO> listByStudent = _appContext.MarksAndVisited
                .Where(n => n.Student.Name.Equals(studentDto.Name))
                .Include(n => n.Homework)
                .ThenInclude(n => n.Lection.Lector.University)
                .Include(n=>n.Student)
                .ThenInclude(n=>n.University)
                .Select(n => new StudentLectionVisitedDto
                {
                    StudentDto = StudentRepository.ToIStudentDTO(n.Student),
                    LectionDto = LectionRepository.ToILectionDTO(n.Homework.Lection),
                    isVisited = n.IsVisited
                });
            _logger.LogInformation("StudentVisitedReport in Database");
            return listByStudent.ToList();
        }

        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedReportByLection([NotNull] ILectionDTO lectionDto)
        {
            new LectionRepository(_appContext, _loggerLection, _loggerLector, _loggerUniversity).CheckConcreteLectionDontExist(LectionRepository.ToLection(lectionDto).Name);
            IEnumerable<IStudentLectionVisitedDTO> listByLection = _appContext.MarksAndVisited
                .Where(n => n.Homework.Lection.Name.Equals(lectionDto.Name))
                .Include(n => n.Homework)
                .ThenInclude(n => n.Lection.Lector.University)
                .Include(n=>n.Student)
                .ThenInclude(n=>n.University)
                .Select(n => new StudentLectionVisitedDto
                {
                    StudentDto = StudentRepository.ToIStudentDTO(n.Student),
                    LectionDto = LectionRepository.ToILectionDTO(n.Homework.Lection),
                    isVisited = n.IsVisited
                });
            _logger.LogInformation("LectionVisitedReport in Database");
            return listByLection.ToList();
        }
        
    }
}