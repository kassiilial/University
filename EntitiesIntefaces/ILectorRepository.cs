using System.Collections.Generic;
using DataTransferObject;

namespace EntitiesInterfaces
{
    public interface ILectorRepository
    {
        public void Create(ILectorDTO dto);
        public void Delete(ILectorDTO dto);
        public void Update(ILectorDTO lectorDto, string newName);
        public List<ILectorDTO> GetAll();
        public ILectorDTO Get(string name);
    }
}