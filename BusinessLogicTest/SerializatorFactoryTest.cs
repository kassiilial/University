using System;
using BusinessLogic.Services;
using BusinessLogicInterfaces;
using NUnit.Framework;

namespace BusinessLogicTest
{
    
    public class SerializatorFactoryTest
    {
        
        [TestCase(MySerializator.Json,  typeof(SerializatorToJson))]
        [TestCase(MySerializator.XML, typeof(SerializatorToXML))]
        [TestCase(MySerializator.YAML, typeof(SerializatorToYAML))]
        public void MakeChoiceOfFormat_DifferetnSerializatorTypeInput_DefferetSerilisatorOutput(MySerializator format, Type concreteSerialisator)
        {
            //arrange
            ISerializator serializator = null;
            ISerializatorFactory serializatorFactory = new SerializatorFactory();
            
            //act
            serializator = serializatorFactory.MakeChoiceOfFormat(format);

            //assert
            Assert.That(serializator.GetType(), Is.EqualTo(concreteSerialisator));
        }
    }
}