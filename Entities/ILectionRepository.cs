using System.Collections.Generic;
using DataTransferObject;

namespace Entities
{
    public interface ILectionRepository
    {
        public void Create(ILectionDTO lectionDto);
        public void Delete(ILectionDTO lectionDto);
        public void Update(ILectionDTO lectionDto, string newName);
        public List<ILectionDTO> GetAll();
        public ILectionDTO Get(string name);
    }
}