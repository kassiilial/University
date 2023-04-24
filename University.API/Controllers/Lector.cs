using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Lector : Controller
    {
        private readonly ILogger<Lector> _logger;
        private readonly IUniversityServices _universityServices;
        private readonly ILectorServices _lectorServices;

        public Lector(ILogger<Lector> logger, IUniversityServices universityServices, ILectorServices lectorServices)
        {
            _logger = logger;
            _universityServices = universityServices;
            _lectorServices = lectorServices;
        }

        ///<summary>Получение списка лекторов</summary>
        [HttpGet]
        public List<ILectorDTO> GetAllItems()
        {
            _logger.LogInformation("Get all ILector");
            return _lectorServices.GetAll();
        }
        
        ///<summary>Получение информации о лекторе</summary>
        [HttpGet("name")]
        public ILectorDTO GetItems(string name)
        {
            _logger.LogInformation("Get ILector");
            return _lectorServices.Get(name);
        }

        [HttpPost("universityName, lectorName")]
        public void PostNewItem(string universityName, string lectorName)
        {
            IUniversityDTO universityDto = _universityServices.Get(universityName);
            _lectorServices.Create(universityDto, lectorName);
            _logger.LogInformation("Post ILector");
        }
        
        ///<summary>Обновление имени лектора</summary>
        [HttpPut("oldName, newName")]
        public void PutNewItem(string oldName, string newName)
        {
            ILectorDTO lectorDto = _lectorServices.Get(oldName);
            _lectorServices.Update(lectorDto ,newName);
            _logger.LogInformation("Put ILector");
        }

        ///<summary>Удаление лектора</summary>
        [HttpDelete("name")]
        public void DeleteItem(string name)
        {
            ILectorDTO lectorDto = _lectorServices.Get(name);
            _lectorServices.Delete(lectorDto);
            _logger.LogInformation("Delete ILector");
        }
    }
}