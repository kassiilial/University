namespace BusinessLogicInterfaces
{
    public interface IVisitReport
    {
        public string GenerateSerialisedVisitReport(object obj, MySerializator serializator);
    }
    public enum MySerializator
    {
        XML=0,
        Json=1,
        YAML=2
    }
}