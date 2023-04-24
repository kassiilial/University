namespace DataTransferObject 
{
    public interface IStudentHomeworkMarksDTO
    {
        public IStudentDTO StudentDto { get; set; }
        public IHomeworkDTO HomeworkDto { get; set; }
        public int Mark { get; set; }
    }

    public class StudentHomeworkMarksDto:IStudentHomeworkMarksDTO
    {
        public IStudentDTO StudentDto { get; set; }
        public IHomeworkDTO HomeworkDto { get; set; }
        public int Mark { get; set; } = -1;
    }
}