using System.Collections.Generic;
using DataTransferObject;

namespace EntitiesInterfaces
{
    public interface IHomeworkRepository
    {
        public void Create(IHomeworkDTO homeworkDto);
        public void Delete(IHomeworkDTO homeworkDto);
        public void Update(IHomeworkDTO homeworkDto, string newName);
        public List<IHomeworkDTO> GetAll();
        public IHomeworkDTO Get(string name);
        public IHomeworkDTO GetByLection(ILectionDTO lectionDto);
    }
}