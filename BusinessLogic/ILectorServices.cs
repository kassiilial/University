using System.Collections.Generic;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface ILectorServices
    {
        public void Create(IUniversityDTO universityDto, string name);
        public List<ILectorDTO> GetAll();
        public ILectorDTO Get(string name);
        public void Update(ILectorDTO lectorDto, string name);
        public void Delete(ILectorDTO lectorDto);
    }
}