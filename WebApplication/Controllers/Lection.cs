using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Lection : Controller
    {
        private readonly ILogger<Lection> _logger;
        private readonly ILectionServices _lectionServices;
        private readonly ILectorServices _lectorServices;

        public Lection(ILogger<Lection> logger, ILectionServices lectionServices, ILectorServices lectorServices)
        {
            _logger = logger;
            _lectionServices = lectionServices;
            _lectorServices = lectorServices;
        }

        ///<summary>Получение списка всех лекций</summary>
        [HttpGet]
        public List<ILectionDTO> GetAllItems()
        {
            _logger.LogInformation("Get all ILection");
            return _lectionServices.GetAll();
        }
        
        ///<summary>Получение информации по лекции</summary>
        [HttpGet("name")]
        public ILectionDTO GetItems(string name)
        {
            _logger.LogInformation("Get ILection by Name");
            return _lectionServices.Get(name);
        }

        ///<summary>Создание новой лекции</summary>
        [HttpPost("lectorName, lectionName")]
        public void PostNewItem(string lectorName, string lectionName)
        {
            ILectorDTO lectorDto = _lectorServices.Get(lectionName);
            _lectionServices.Create(lectorDto, lectionName);
            _logger.LogInformation("Post ILection");
        }
        
        ///<summary>Переименование лекции</summary>
        [HttpPut("oldName, newName")]
        public void PutNewItem(string oldName, string newName)
        {
            //ILectionServices services = new LectionServices(_logger);
            ILectionDTO lectionDto = _lectionServices.Get(oldName);
            _lectionServices.Update(lectionDto ,newName);
            _logger.LogInformation("Put ILection");
        }

        ///<summary>Удаление лекции</summary>
        [HttpDelete("name")]
        public void DeleteItem(string name)
        {
            //ILectionServices services = new LectionServices(_logger);
            ILectionDTO lectionDto = _lectionServices.Get(name);
            _lectionServices.Delete(lectionDto);
            _logger.LogInformation("Delete ILection");
        }
    }
}