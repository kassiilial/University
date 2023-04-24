using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Student : Controller
    {
        private readonly ILogger<Student> _logger;
        private readonly IStudentServices _studentServices;
        private readonly IUniversityServices _universityServices;

        public Student(ILogger<Student> logger, IStudentServices studentServices, IUniversityServices universityServices)
        {
            _logger = logger;
            _studentServices = studentServices;
            _universityServices = universityServices;
        }

        ///<summary>Получение списка студентов</summary>
        [HttpGet]
        public List<IStudentDTO> GetAll()
        {
            _logger.LogInformation("Get all IStudent");
            return _studentServices.GetAll();
        }
        
        ///<summary>Получение информации о студентах</summary>
        [HttpGet("name")]
        public IStudentDTO GetItems(string name)
        {
            _logger.LogInformation("Get IStudent");
            return _studentServices.Get(name);
        }

        ///<summary>Создание новго студента</summary>
        [HttpPost("Student Name, Univestiry Name")]
        public void PostNewItem(string studentName, string univesrityName)
        {
            _studentServices.Create(_universityServices.Get(univesrityName), studentName);
            _logger.LogInformation("Post IStudent");
        }
        
        ///<summary>Обновление имени студента</summary>
        [HttpPut("oldName, newName")]
        public void PutNewItem(string oldName, string newName)
        {
            IStudentDTO studentDTO = _studentServices.Get(oldName);
            _studentServices.Update(studentDTO ,newName);
            _logger.LogInformation("Put IStudent");
        }

        ///<summary>Удаление студента</summary>
        [HttpDelete("name")]
        public void DeleteItem(string name)
        {
            IStudentDTO studentDTO = _studentServices.Get(name);
            _studentServices.Delete(studentDTO);
            _logger.LogInformation("Delete IStudent");
        }
    }
}