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
    public class LectionRepository:ILectionRepository
    {
        private readonly ILogger<LectionRepository> _logger;
        private readonly ILogger<LectorRepository> _loggerLector;
        private readonly ILogger<UniversityRepository> _loggerUniversity;
        private readonly AppContext _appContext;

        public LectionRepository( [NotNull]AppContext appContext, [NotNull]ILogger<LectionRepository> logger,
            [NotNull]ILogger<LectorRepository> loggerLector,
            [NotNull]ILogger<UniversityRepository> loggerUniversity)
        {
            _logger = logger;
            _loggerLector = loggerLector;
            _loggerUniversity = loggerUniversity;
            _appContext = appContext;
        }
        
        public void Create(ILectionDTO lectionDto)
        {
            Lection lection = ToLection(lectionDto);
            new LectorRepository(_loggerLector, _appContext, _loggerUniversity).CheckConcreteLectorDontExist(lection.Lector.Name);
            CheckConcreteLectionExist(lection.Name);
            _appContext.Lections.Add(lection);
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedLection);
        }
        
        public void Delete([NotNull]ILectionDTO lectionDto)
        {
            Lection lection = ToLection(lectionDto);
            CheckConcreteLectionDontExist(lection.Name);
            _appContext.Lections
                .Remove(_appContext.Lections.First(n => n.Name.Equals(lection.Name)));
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.DeletedLection);
        }
        
        public void Update([NotNull]ILectionDTO lectionDto, [NotNull] string newName)
        {
            CheckConcreteLectionDontExist(lectionDto.Name);
            _appContext.Lections.First(n => n.Name.Equals(lectionDto.Name)).Name = newName;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdatedLection);
        }
        
        public List<ILectionDTO> GetAll()
        {
            CheckLectionsExist();
            var lectionsList = _appContext.Lections.Select(n => n).ToList();
            List<ILectionDTO> lectionDto = new List<ILectionDTO>();
            foreach (var one in lectionsList)
            {
                lectionDto.Add(Get(one.Name));
            }
            _logger.LogInformation(LoggingText.ReadedLection);
            return lectionDto;
        }
        
        public ILectionDTO Get([NotNull]string name)
        {
            CheckConcreteLectionDontExist(name);
            var concretLection = _appContext.Lections
                .Include(n=>n.Lector)
                .ThenInclude(n=>n.University)
                .First(n => n.Name.Equals(name));
            ILectionDTO lectionDto = new LectionDto
            {
                Name = concretLection.Name,
                LectorDto = new LectorDto
                {
                    Name = concretLection.Lector.Name,
                    UniversityDtoForWork = new UniversityDto
                    {
                        Name = concretLection.Lector.University.Name
                    }
                }
            };
            _logger.LogInformation(LoggingText.ReadedLection);
            return lectionDto;
        }
        
        internal void CheckConcreteLectionDontExist(string name)
        {
            CheckLectionsExist();
            if (!_appContext.Lections.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemMissingException(ExceptionText.InvalidLection);
            }
        }
        
        internal void CheckConcreteLectionExist(string name)
        {
            CheckLectionsExist();
            if (_appContext.Lections.Any(n=>n.Name.Equals(name)))
            {
                if (_appContext.Lections.Any(n=>n.Name.Equals(name)))
                {
                    throw new ItemAlreadyExistsException(ExceptionText.LectionExist);
                }
            }
        }
        
        internal void CheckLectionsExist()
        {
            if (!_appContext.Lections.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyLection);
            }
        }
        
        internal static ILectionDTO ToILectionDTO([NotNull]Lection lection)
        {
            ILectionDTO lectionDto = new LectionDto()
            {
               Name = lection.Name,
               LectorDto = LectorRepository.ToILectorDTO(lection.Lector)
            };
            return lectionDto;
        }

        internal static Lection ToLection(ILectionDTO lectionDto)
        {
            Lection lection = new Lection()
            {
                Name = lectionDto.Name,
                Lector = LectorRepository.ToLector(lectionDto.LectorDto)
            };
            return lection;
        }
    }
}