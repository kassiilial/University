using System;
using System.Collections.Generic;
using DataTransferObject;

namespace BusinessLogicInterfaces
{
    public interface ILectionServices
    {
        public void Create(ILectorDTO lectorDto, string name);
        public List<ILectionDTO> GetAll();
        public ILectionDTO Get(string name);
        public void Update(ILectionDTO lectionDto, string name);
        public void Delete(ILectionDTO lectionDto);
        public List<IStudentLectionVisitedDTO> GetMarkAndVisitedByLection(ILectionDTO lectionDto);
    }
}