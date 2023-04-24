using System;
using BusinessLogicInterfaces;
using NUnit.Framework;
using University.BusinessLogic.Services.ReportServices;

namespace BusinessLogicTest
{
    
    public class SerializatorFactoryTest
    {
        
        [TestCase(Serialization.Json,  typeof(SerializatorToJson))]
        [TestCase(Serialization.Xml, typeof(SerializatorToXML))]
        [TestCase(Serialization.Yaml, typeof(SerializatorToYAML))]
        public void MakeChoiceOfFormat_DifferetnSerializatorTypeInput_DefferetSerilisatorOutput(Serialization format, Type concreteSerialisator)
        {
            //arrange
            ISerializator serializator = null;
            var serializatorFactory = new SerializatorFactory();
            
            //act
            serializator = serializatorFactory.MakeChoiceOfFormat(format);

            //assert
            Assert.That(serializator.GetType(), Is.EqualTo(concreteSerialisator));
        }
    }
}