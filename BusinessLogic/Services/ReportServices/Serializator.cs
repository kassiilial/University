using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;

namespace BusinessLogic.Services
{
    public interface ISerializator
        {
            public string Serialize(object obj);
        }
        
        internal class SerializatorToXML:ISerializator
        {
            internal SerializatorToXML()
            {
            }
            
            public string Serialize([NotNull]object obj)
            {
                using StringWriter textWriter = new StringWriter();
                
                if (obj is StudentReport studentReport)
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(StudentReport));
                    formatter.Serialize(textWriter, studentReport);
                    return textWriter.ToString();
                }
                if (obj is LectionReport lectionReport)
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(LectionReport));
                    formatter.Serialize(textWriter, lectionReport);
                    return textWriter.ToString();
                }
                throw new System.Exception("Не был выбран тип объекта для сериализации");
            }
        }
        
        internal class SerializatorToJson : ISerializator
        {
            internal SerializatorToJson()
            {
            }
            public string Serialize(object obj)
            {
                string result = null;
                if (obj is StudentReport studentReport)
                {
                    result = JsonSerializer.Serialize(studentReport);
                }
                if (obj is LectionReport lectionReport)
                {
                    result = JsonSerializer.Serialize(lectionReport);
                }
                return result;
            }
        }
        
        internal class SerializatorToYAML : ISerializator
        {
           
    
            internal SerializatorToYAML()
            {
            }
    
            public string Serialize(object obj)
            {
                var serializer = new SerializerBuilder()
                    .Build();
                using StringWriter textWriter = new StringWriter();
                if (obj is StudentReport studentReport)
                {
                    serializer.Serialize(textWriter,studentReport);
                }
                if (obj is LectionReport lectionReport)
                {
                    serializer.Serialize(textWriter, lectionReport);
                }
                return textWriter.ToString();
            }
        }
}