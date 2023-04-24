namespace BusinessLogicInterfaces
{
    public interface IVisitReport
    {
        public string GenerateSerialisedVisitReport(object obj, Serialization serializator);
    }
}