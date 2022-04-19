namespace DataTransferObject
{
    public interface IStudentLectionVisitedDTO
    {
        public IStudentDTO StudentDto { get; set; }
        public ILectionDTO LectionDto { get; set; }
        public bool isVisited { get; set; }
    }

    public class StudentLectionVisitedDto:IStudentLectionVisitedDTO
    {
        public IStudentDTO StudentDto { get; set; }
        public ILectionDTO LectionDto { get; set; }
        public bool isVisited { get; set; }
    }
}