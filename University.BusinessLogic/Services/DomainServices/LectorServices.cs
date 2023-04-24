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
    public class LectorServices:ILectorServices
    {
        private ILectorRepository _lectorRepository;
        private readonly ILogger<LectorServices> _logger;

        public LectorServices([NotNull]ILogger<LectorServices> logger, [NotNull]ILectorRepository lectorRepository)
        {
            _logger = logger;
            _lectorRepository = lectorRepository;
        }     
        public void Create([NotNull]IUniversityDTO universityDto, [NotNull]string name)
        {
            ILectorDTO lectorDto = new LectorDto
            {
                Name = name,
                UniversityDtoForWork = universityDto
            };
            _lectorRepository.Create(lectorDto);
            _logger.LogInformation(ResLoggingText.AddedLector);
        }
        public List<ILectorDTO> GetAll()
        {
            _logger.LogInformation(ResLoggingText.ReadedLector);
            return _lectorRepository.GetAll();
        }
        public ILectorDTO Get([NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.ReadedLector);
            return _lectorRepository.Get(name);
        }
        public void Update([NotNull]ILectorDTO lectorDto, [NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.UpdatedLector);
            _lectorRepository.Update(lectorDto, name);
        }
        public void Delete([NotNull]ILectorDTO lectorDto)
        {
            _logger.LogInformation(ResLoggingText.DeletedLector);
            _lectorRepository.Delete(lectorDto);
        }
    }
}