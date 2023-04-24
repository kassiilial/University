using System.Collections.Generic;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface IHomeworkServices
    {
        public void Create(ILectionDTO lectionDto, string name);
        public List<IHomeworkDTO> GetAll();
        public IHomeworkDTO Get(string name);
        public IHomeworkDTO GetByLection(ILectionDTO lectionDto);
        public void Update(IHomeworkDTO homeworkDto, string name);
        public void Delete(IHomeworkDTO homeworkDto);
    }
}