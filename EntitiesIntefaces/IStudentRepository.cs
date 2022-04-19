using System.Collections.Generic;
using DataTransferObject;

namespace EntitiesInterfaces
{
    public interface IStudentRepository
    {
        public void Create(IStudentDTO studentDto);
        public void Delete(IStudentDTO studentDto);
        public void Update(IStudentDTO studentDto, string newName);
        public List<IStudentDTO> GetAll();
        public IStudentDTO Get(string name);
    }
}