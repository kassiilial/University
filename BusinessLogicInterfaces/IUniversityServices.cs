using System.Collections.Generic;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface IUniversityServices
    {
        public void Create(string name);
        public List<IUniversityDTO> GetAll();
        public IUniversityDTO Get(string name);
        public void Update(IUniversityDTO universityDto, string name);
        public void Delete(IUniversityDTO universityDto);
    }
}