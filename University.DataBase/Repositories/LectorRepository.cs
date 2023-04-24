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
    public class LectorRepository:ILectorRepository
    {
        private readonly ILogger<LectorRepository> _logger;
        private readonly ILogger<UniversityRepository> _loggerUniversity;
        private readonly AppContext _appContext;
        public LectorRepository([NotNull]ILogger<LectorRepository> logger, [NotNull]AppContext appContext,
            [NotNull]ILogger<UniversityRepository> loggerUniversity)
        {
            _logger = logger;
            _appContext = appContext;
            _loggerUniversity = loggerUniversity;
        }
        public void Create([NotNull]ILectorDTO lectorDto)
        {
            Lector lector = ToLector(lectorDto);
            CheckConcreteLectorExist(lector.Name);
            new UniversityRepository(_loggerUniversity,_appContext).CheckConcreteUniversityDontExist(lector.University.Name);
            _appContext.Lectors.Add(lector);
            _appContext.SaveChanges();
            _logger.LogInformation("Create IUniversity in Database");
        }
        public void Delete([NotNull]ILectorDTO lectorDto)
        {
            Lector lector = ToLector(lectorDto);
            CheckConcreteLectorDontExist(lector.Name);
            _appContext
                .Lectors
                .Remove(_appContext.Lectors.First(n => n.Name.Equals(lector.Name)));
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.DeletedLector);
        }
        public void Update([NotNull]ILectorDTO lectorDto, [NotNull]string newName)
        {
            CheckConcreteLectorDontExist(lectorDto.Name);
            _appContext
                .Lectors
                .First(n => n.Name.Equals(lectorDto.Name))
                .Name = newName;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdatedLector);
        }
        public List<ILectorDTO> GetAll()
        {
           CheckLectorsExist();
            var lectorsList = _appContext
                .Lectors.Select(n=>n)
                .ToList();
            List<ILectorDTO> dto = new List<ILectorDTO>();
            foreach (var one in lectorsList)
            {
                dto.Add(Get(one.Name));
            }
            _logger.LogInformation(LoggingText.ReadedLector);
            return dto;
        }
        public ILectorDTO Get([NotNull]string name)
        {
            CheckConcreteLectorDontExist(name);
            var concreteLector = _appContext.Lectors
                .Include(n=>n.University)
                .First(n => n.Name.Equals(name));
            ILectorDTO lectorDto = new LectorDto
            {
                Name = concreteLector.Name,
                UniversityDtoForWork = new UniversityDto
                {
                    Name = concreteLector.University.Name
                }
            };
            _logger.LogInformation(LoggingText.ReadedLector);
            return lectorDto;
        }
        
        internal void CheckConcreteLectorDontExist(string name)
        {
            CheckLectorsExist();
            if (!_appContext.Lectors.Any(n=>n.Name.Equals(name)))
            {
                 throw new ItemMissingException(ExceptionText.InvalidLection);
            }
        }
        
        internal void CheckConcreteLectorExist(string name)
        {
            CheckLectorsExist();
            if (_appContext.Lectors.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemAlreadyExistsException(ExceptionText.LectionExist);
            }
        }
        
        internal void CheckLectorsExist()
        {
            if (!_appContext.Lectors.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyLection);
            }
        }
        
        internal static ILectorDTO ToILectorDTO(Lector lector)
        {
            ILectorDTO lectorDto = new LectorDto()
            {
                Name = lector.Name,
                UniversityDtoForWork = UniversityRepository.ToIUniversityDTO(lector.University)
            };
            return lectorDto;
        }

        internal static Lector ToLector(ILectorDTO lectorDto)
        {
            Lector lector = new Lector()
            {
                Name = lectorDto.Name,
                University = UniversityRepository.ToUniversity(lectorDto.UniversityDtoForWork)
            };
            return lector;
        }
    }
}