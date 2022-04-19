using BusinessLogicInterfaces;

namespace BusinessLogic.Services
{
    interface ISerializatorFactory
    {
        public ISerializator MakeChoiceOfFormat(MySerializator format);
    }
    public class SerializatorFactory : ISerializatorFactory
    {
        public SerializatorFactory()
        {
        }
        public ISerializator MakeChoiceOfFormat(MySerializator format)
        {
            ISerializator serializator = null;
            switch (format)
            {
                case MySerializator.XML:
                    serializator = new SerializatorToXML();
                    break;
                case MySerializator.Json:
                    serializator = new SerializatorToJson();
                    break;
                case MySerializator.YAML:
                    serializator = new SerializatorToYAML();
                    break;
            }
            return serializator;
        }
    }
}