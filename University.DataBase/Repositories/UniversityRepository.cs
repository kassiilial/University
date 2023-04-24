using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataTransferObject;
using Entities.Entities;
using Entities.Repositories.Exception;
using Entities;
using Microsoft.Extensions.Logging;

namespace Entities.Repositories
{
    public class UniversityRepository:IUniversityRepository
    {
        private readonly ILogger<UniversityRepository> _logger;
        private readonly AppContext _appContext;
        public UniversityRepository([NotNull]ILogger<UniversityRepository> logger, [NotNull]AppContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }
        
        public void Create([NotNull]IUniversityDTO newUniversity)
        {
            CheckConcreteUniversityExist(newUniversity.Name);
            University university = ToUniversity(newUniversity);
            _appContext.Universities.Add(university);
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.AddedUniversity);
        }
        
        public void Delete([NotNull]IUniversityDTO universityDto)
        {
            CheckConcreteUniversityDontExist(universityDto.Name);
            _appContext
                .Universities
                .Remove(_appContext.Universities.First(n=>n.Name.Equals(ToUniversity(universityDto).Name)));
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.DeletedUniversity);
        }
        
        public void Update([NotNull]IUniversityDTO universityDto, [NotNull]string newName)
        {
            CheckConcreteUniversityDontExist(universityDto.Name);
            _appContext
                .Universities
                .First(n => n.Name.Equals(universityDto.Name))
                .Name = newName;
            _appContext.SaveChanges();
            _logger.LogInformation(LoggingText.UpdatedUniversity);
        }
        
        public List<IUniversityDTO> GetAll()
        {
           CheckUniversitiesExist();
            List<IUniversityDTO> universityDtos = new List<IUniversityDTO>();
            var forExchange =  _appContext.Universities.Select(n =>n).ToList();
            foreach (var one in forExchange)
            {
                universityDtos.Add(Get(one.Name));
            }
            _logger.LogInformation(LoggingText.ReadedUniversity);
            return universityDtos;
        }
        
        public IUniversityDTO Get([NotNull]string name)
        {
            CheckConcreteUniversityDontExist(name);
            IUniversityDTO dto = new UniversityDto
            {
                Name = _appContext
                    .Universities
                    .First(n => n.Name.Equals(name))
                    .Name
            };
            _logger.LogInformation(LoggingText.ReadedUniversity);
            return dto;
        }
        
        internal void CheckConcreteUniversityDontExist(string name)
        {
            CheckUniversitiesExist();
            if (!_appContext.Universities.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemMissingException(ExceptionText.InvalidUniversity);
            }
        }
        
        internal void CheckConcreteUniversityExist(string name)
        {
            if (_appContext.Universities.Any(n=>n.Name.Equals(name)))
            {
                throw new ItemAlreadyExistsException(ExceptionText.StudentExist);
            }
        }
        
        internal void CheckUniversitiesExist()
        {
            if (!_appContext.Universities.Any())
            {
                throw new ItemMissingException(ExceptionText.EmptyUniversity);
            }
        }
        
        internal static IUniversityDTO ToIUniversityDTO(University university)
        {
            IUniversityDTO universityDto = new UniversityDto
            {
                Name = university.Name
            };
            return universityDto;
        }

        internal static University ToUniversity(IUniversityDTO universityDto)
         {
             University university = new University
             {
                 Name = universityDto.Name
             };
             return university;
         }
    }
}