using BusinessLogicInterfaces;

namespace University.BusinessLogic.Services.ReportServices
{
    interface ISerializatorFactory
    {
        public ISerializator MakeChoiceOfFormat(Serialization format);
    }
    public class SerializatorFactory : ISerializatorFactory
    {
        public SerializatorFactory()
        {
        }
        public ISerializator MakeChoiceOfFormat(Serialization format)
        {
            ISerializator serializator = null;
            switch (format)
            {
                case Serialization.Xml:
                    serializator = new SerializatorToXML();
                    break;
                case Serialization.Json:
                    serializator = new SerializatorToJson();
                    break;
                case Serialization.Yaml:
                    serializator = new SerializatorToYAML();
                    break;
            }
            return serializator;
        }
    }
}