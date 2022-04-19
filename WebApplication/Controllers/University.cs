using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class University : Controller
    {
        private readonly ILogger<University> _logger;
        private readonly IUniversityServices _universityServices;

        public University(ILogger<University> logger, IUniversityServices universityServices)
        {
            _logger = logger;
            _universityServices = universityServices;
        }

        ///<summary>Получение списка университетов</summary>
        [HttpGet]
        public List<IUniversityDTO> GetAllItems()
        {
            _logger.LogInformation("Get all IUniversity");
            return _universityServices.GetAll();
        }
        
        ///<summary>Получение информации об университете</summary>
        [HttpGet("name")]
        public IUniversityDTO GetItems(string name)
        {
            _logger.LogInformation("Get IUniversity");
            return _universityServices.Get(name);
        }

        ///<summary>Создание университета</summary>
        [HttpPost("name")]
        public void PostNewItem(string name)
        {
           
            _universityServices.Create(name);
             _logger.LogInformation("Post IUniversity");
        }
        
        ///<summary>Обнавление имени университета</summary>
        [HttpPut("oldName, newName")]
        public void PutNewItem(string oldName, string newName)
        {
            IUniversityDTO universityDto = _universityServices.Get(oldName);
            _universityServices.Update(universityDto ,newName);
            _logger.LogInformation("Put IUniversity");
        }

        ///<summary>Удаление университета</summary>
        [HttpDelete("name")]
        public void DeleteItem(string name)
        {
            IUniversityDTO universityDto = _universityServices.Get(name);
            _universityServices.Delete(universityDto);
            _logger.LogInformation("Delete IUniversity");
        }
    }
}