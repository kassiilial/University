using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Resources;
using BusinessLogicInterfaces;
using DataTransferObject;
using Entities.Repositories;
using Entities;
using Microsoft.Extensions.Logging;

namespace University.BusinessLogic.Services.DomainServices
{
    public class UniversityServices : IUniversityServices
    {
        private IUniversityRepository _universityRepository;
        private readonly ILogger<UniversityServices> _logger;
        public UniversityServices([NotNull]ILogger<UniversityServices> logger, 
            [NotNull]IUniversityRepository universityRepository)
        {
            _logger = logger;
            _universityRepository = universityRepository;
        }
        public void Create([NotNull]string name)
        {
            IUniversityDTO universityDto = new UniversityDto(name);
            _universityRepository.Create(universityDto);
            _logger.LogInformation(ResLoggingText.AddedUniversity);
        }
        public List<IUniversityDTO> GetAll()
        {
            _logger.LogInformation(ResLoggingText.ReadedUniversity);
            return _universityRepository.GetAll();
        }
        public IUniversityDTO Get([NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.ReadedUniversity);
            return _universityRepository.Get(name);
        }
        public void Update([NotNull]IUniversityDTO universityDto, [NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.UpdatedUniversity);
            _universityRepository.Update(universityDto, name);
        }
        public void Delete([NotNull]IUniversityDTO universityDto)
        {
            _logger.LogInformation(ResLoggingText.DeletedUniversity);
            _universityRepository.Delete(universityDto);
        }
    }
}