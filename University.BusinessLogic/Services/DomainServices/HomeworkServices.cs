using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Exception;
using BusinessLogic.Resources;
using BusinessLogicInterfaces;
using DataTransferObject;
using Entities;
using Entities.Repositories;
using Microsoft.Extensions.Logging;

namespace University.BusinessLogic.Services.DomainServices
{
    public class HomeworkServices:IHomeworkServices
    {
        private IHomeworkRepository _homeworkRepository;
        private readonly ILogger<HomeworkServices> _logger;
        public HomeworkServices([NotNull]ILogger<HomeworkServices> logger,
            [NotNull]IHomeworkRepository homeworkRepository)
        {
            _logger = logger;
            _homeworkRepository = homeworkRepository;
        }
        public void Create([NotNull]ILectionDTO lectionDto,[NotNull]string name)
        {
            IHomeworkDTO homeworkDto = new HomeworkDto
            {
                Name = name,
                LectionDto = lectionDto
            };
            _homeworkRepository.Create(homeworkDto);
            _logger.LogInformation(ResLoggingText.AddedHomework);
        }
        public List<IHomeworkDTO> GetAll()
        {
            _logger.LogInformation(ResLoggingText.ReadedHomework);
            return _homeworkRepository.GetAll();
        }
        public IHomeworkDTO Get([NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.ReadedHomework);
            return _homeworkRepository.Get(name);
        }
        public IHomeworkDTO GetByLection([NotNull]ILectionDTO lectionDto)
        {
            _logger.LogInformation(ResLoggingText.ReadedHomework);
            return _homeworkRepository.GetByLection(lectionDto);
        }
        public void Update([NotNull]IHomeworkDTO homeworkDto, [NotNull]string name)
        {
            _logger.LogInformation(ResLoggingText.UpdatedHomework);
            _homeworkRepository.Update(homeworkDto, name);
        }
        public void Delete([NotNull]IHomeworkDTO homeworkDto)
        {
            _logger.LogInformation(ResLoggingText.DeletedHomework);
            _homeworkRepository.Delete(homeworkDto);
        }
    }
}