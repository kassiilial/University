using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Exception;
using BusinessLogic.Resources;
using BusinessLogicInterfaces;
using DataTransferObject;
using Entities.Repositories;
using Entities;
using Microsoft.Extensions.Logging;

namespace University.BusinessLogic.Services.DomainServices
{
    public class LectionServices:ILectionServices
    {
        private ILectionRepository _lectionRepository;
        private readonly ILogger<LectionServices> _logger;
        private readonly IMarkAndVisitedRepository _markAndVisitedRepository;
        public LectionServices([NotNull]ILogger<LectionServices> logger, 
            [NotNull]ILectionRepository lectionRepository,
        [NotNull]IMarkAndVisitedRepository markAndVisitedRepository)
        {
            _logger = logger;
            _lectionRepository = lectionRepository;
            _markAndVisitedRepository = markAndVisitedRepository;
        }    
        public void Create([NotNull]ILectorDTO lectorDto ,[NotNull]string name)
        {
            ILectionDTO lectionDto = new LectionDto
            {
                Name = name,
                LectorDto = lectorDto
            };
            _lectionRepository.Create(lectionDto);
            _logger.LogInformation(ResLoggingText.AddedLection);
        }
        public List<ILectionDTO> GetAll()
        {
            _logger.LogInformation(ResLoggingText.ReadedLection);
            return _lectionRepository.GetAll();
        }
        public ILectionDTO Get([NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.ReadedLection);
            return _lectionRepository.Get(name);
        }
        public void Update([NotNull]ILectionDTO lectionDto, [NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.UpdatedLection);
            _lectionRepository.Update(lectionDto, name);
        }
        public void Delete([NotNull]ILectionDTO lectionDto)
        {
            _logger.LogInformation(ResLoggingText.DeletedLection);
            _lectionRepository.Delete(lectionDto);
        }
        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByLection([NotNull]ILectionDTO lectionDto)
        {
            _logger.LogInformation("Ask info for LectionVisitedReport");
            return _markAndVisitedRepository.GetMarkAndVisitedReportByLection(lectionDto);
        }
    }
}