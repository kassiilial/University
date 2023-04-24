using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class Homework : Controller
    {
        private readonly ILogger<Homework> _logger;
        private readonly IHomeworkServices _homeworkServices;
        private readonly ILectionServices _lectionServices;

        public Homework(ILogger<Homework> logger, IHomeworkServices homeworkServices, ILectionServices lectionServices)
        {
            _logger = logger;
            _homeworkServices = homeworkServices;
            _lectionServices = lectionServices;
        }

        ///<summary>Список всех домашних работ</summary>
        [HttpGet]
        public List<IHomeworkDTO> GetAll()
        {
            _logger.LogInformation("Get all IHomework");
            return _homeworkServices.GetAll();
        }
        
        ///<summary>Получение информации о конкретной домашней работе</summary>
        [HttpGet("name")]
        public IHomeworkDTO GetItems(string name)
        {
            _logger.LogInformation("Get IHomework by Name");
            return _homeworkServices.Get(name);
        }

        ///<summary>Создание новой домашней работы</summary>
        [HttpPost("lectionName , homeworkName")]
        public void PostNewItem(string lectionName, string homeworkName)
        {
            ILectionDTO lectionDto = _lectionServices.Get(lectionName);
            _homeworkServices.Create(lectionDto, homeworkName);
            _logger.LogInformation("Post IHomework");
        }
        
        ///<summary>Обновление имени для домашней работы</summary>
        [HttpPut("oldName, newName")]
        public void PutNewItem(string oldName, string newName)
        {
            IHomeworkDTO homeworkDto = _homeworkServices.Get(oldName);
            _homeworkServices.Update(homeworkDto ,newName);
            _logger.LogInformation("Put IHomework");
        }

        ///<summary>Удаление домашней работы</summary>
        [HttpDelete("name")]
        public void DeleteItem(string name)
        {
            IHomeworkDTO homeworkDto = _homeworkServices.Get(name);
            _homeworkServices.Delete(homeworkDto);
            _logger.LogInformation("Delete IHomework");
        }
    }
}