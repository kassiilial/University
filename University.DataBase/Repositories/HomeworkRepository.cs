using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Entities.Entities;
using DataTransferObject;
using Entities.Repositories.Exception;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Entities.Repositories
{
    public class HomeworkRepository:IHomeworkRepository
    {
        private readonly ILogger<HomeworkRepository> _logger;
        private readonly ILogger<MarkAndVisitedRepository> _loggerMarkAdnVisited;
        private readonly ILogger<StudentRepository> _loggerStudent;
        private readonly ILogger<LectionRepository> _loggerLection;
        private readonly ILogger<LectorRepository> _loggerLector;
        private readonly ILogger<UniversityRepository> _loggerUniversity;
        private readonly AppContext _appContext;
        private readonly IStudentRepository _studentRepository;
        private readonly ILectionRepository _lectionRepository;
        

        public HomeworkRepository([NotNull]ILogger<HomeworkRepository> logger,
            [NotNull]AppContext appContext,
            [NotNull]IStudentRepository studentRepository,
            [NotNull]ILectionRepository lectionRepository,
            [NotNull]ILogger<UniversityRepository> loggerUniversity,
            [NotNull]ILogger<MarkAndVisitedRepository> loggerMarkAdnVisited,
            [NotNull]ILogger<LectionRepository> loggerLection,
            [NotNull]ILogger<LectorRepository> loggerLector,
            [NotNull]ILogger<StudentRepository> loggerStudent)
        {
            _logger = logger;
            _appContext = appContext;
            _studentRepository = studentRepository;
            _lectionRepository = lectionRepository;
            _loggerMarkAdnVisited = loggerMarkAdnVisited;
            _loggerLection = loggerLection;
            _loggerUniversity = loggerUniversity;
            _loggerLector = loggerLector;
            _loggerStudent = loggerStudent;
        }

        public void Create([NotNull]IHomeworkDTO homeworkDto)
        {
            Homework homework = ToHomework(homeworkDto);
            CheckConcreteHomeworkExist(homework.Name);
            new LectionRepository(_appContext,_loggerLection,_loggerLector, _loggerUniversity).CheckConcreteLectionDontExist(homework.Lection.Name);
            _appContext.Homeworks.Add(homework);
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedHomework);
            new MarkAndVisitedRepository(_appContext,_studentRepository, 
                _lectionRepository, _loggerMarkAdnVisited, _loggerStudent, 
                _loggerLection, _loggerLector, _logger, _loggerUniversity).Create(homework);
        }
        
        public void Delete([NotNull]IHomeworkDTO homeworkDto)
        {
            CheckConcreteHomeworkDontExist(homeworkDto.Name);
            _appContext.Homeworks
                .Remove(_appContext.Homeworks
                .First(n => n.Name.Equals(homeworkDto.Name)));
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.DeletedHomework);
        }
        
        public void Update([NotNull]IHomeworkDTO homeworkDto, [NotNull]string newName)
        {
            CheckConcreteHomeworkDontExist(homeworkDto.Name);
            _appContext.Homeworks
                .First(n => n.Name.Equals(homeworkDto.Name))
                .Name = newName;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdatedHomework);
        }
        
        public List<IHomeworkDTO> GetAll()
        {
            CheckHomeworksExist();
            var homeworksList = _appContext.Homeworks
                .Select(n => n)
                .Include(n=>n.Lection)
                .ToList();
            List<IHomeworkDTO> homeworkDtos = new List<IHomeworkDTO>();
            foreach (var one in homeworksList)
            {
                homeworkDtos.Add(Get(one.Name));
            }
            _logger.LogInformation(LoggingText.ReadedHomework);
            return homeworkDtos;
        }
        
        public IHomeworkDTO Get([NotNull]string name)
        {
            CheckConcreteHomeworkDontExist(name);
            var concreteHomework =  _appContext.Homeworks
                .Include(n=>n.Lection)
                .First(n => n.Name.Equals(name));
            IHomeworkDTO homeworkDto = new HomeworkDto
            {
                Name = concreteHomework.Name,
                LectionDto = new LectionRepository(_appContext, _loggerLection, _loggerLector, _loggerUniversity).Get(concreteHomework.Lection.Name)
            };
            _logger.LogInformation(LoggingText.ReadedHomework);
            return homeworkDto;
        }
        
        public IHomeworkDTO GetByLection([NotNull]ILectionDTO lectionDto)
        {
            new LectionRepository(_appContext, _loggerLection, _loggerLector, _loggerUniversity).CheckConcreteLectionDontExist(lectionDto.Name);
            CheckHomeworksExistByLection(lectionDto);
            var concreteHomeworkByLection =  _appContext.Homeworks
                .Include(n=>n.Lection)
                .First(n => n.Lection.Name.Equals(lectionDto.Name));
            IHomeworkDTO homeworkDto = new HomeworkDto
            {
                Name = concreteHomeworkByLection.Name,
                LectionDto = new LectionRepository(_appContext, _loggerLection, _loggerLector, _loggerUniversity).Get(concreteHomeworkByLection.Lection.Name)
            };
            _logger.LogInformation(LoggingText.ReadedHomework);
            return homeworkDto;
        }
        
        internal void CheckHomeworksExistByLection(ILectionDTO lectionDto)
        {
            CheckHomeworksExist();
            if (!_appContext.Homeworks.Any(n => n.Lection.Name.Equals(lectionDto.Name)))
            {
                throw new ItemMissingException(ExceptionText.InvalidHomework);
            }
        }
        
        internal void CheckConcreteHomeworkDontExist(string name)
        {
            CheckHomeworksExist();
            if (!_appContext.Homeworks.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemMissingException(ExceptionText.InvalidHomework);
            }
        }
        
        internal void CheckConcreteHomeworkExist(string name)
        {
            CheckHomeworksExist();
            if (_appContext.Homeworks.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemAlreadyExistsException(ExceptionText.HomeworkExist);
            }
        }
        
        internal void CheckHomeworksExist()
        {
            if (!_appContext.Homeworks.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyHomework);
            }
        }

        
        internal static IHomeworkDTO ToIHomeworkDTO(Homework lection)
        {
            IHomeworkDTO homeworkDto = new HomeworkDto
            {
                Name = lection.Name,
                LectionDto = LectionRepository.ToILectionDTO(lection.Lection)
            };
            return homeworkDto;
        }

        internal static Homework ToHomework(IHomeworkDTO homeworkDto)
        {
            Homework homework = new Homework()
            {
                Name = homeworkDto.Name,
                Lection = LectionRepository.ToLection(homeworkDto.LectionDto)
            };
            return homework;
        }
        
    }
}