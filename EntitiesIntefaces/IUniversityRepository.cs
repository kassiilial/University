using System.Collections.Generic;
using DataTransferObject;

namespace EntitiesInterfaces
{
    public interface IUniversityRepository
    {
        public void Create(IUniversityDTO newUniversity);
        public void Delete(IUniversityDTO universityDto);
        public void Update(IUniversityDTO universityDto, string newName);
        public List<IUniversityDTO> GetAll();
        public IUniversityDTO Get(string name);
        
    }
}