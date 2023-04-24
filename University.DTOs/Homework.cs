namespace DataTransferObject
{
    public interface IHomeworkDTO
    {
        public string Name { get; set; }
        public ILectionDTO LectionDto { get; set; }
    }
    public class HomeworkDto:IHomeworkDTO
    {
        public string Name { get; set; }
        public ILectionDTO LectionDto { get; set; }
        public HomeworkDto(string name)
        {
            Name = name;
        }

        public HomeworkDto()
        {
        }
    }
}