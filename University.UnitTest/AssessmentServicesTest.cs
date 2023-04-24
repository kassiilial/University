using System.Collections.Generic;
using BusinessLogicInterfaces;
using DataTransferObject;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using University.BusinessLogic.Services.DomainServices;

namespace BusinessLogicTest
{
    public class AssessmentServicesTest
    {
        
        
        [Test]
        public void CalculateStudentMissingByLector_InputListOneStudent_outputDynamic()
        {
            //arrange
            //TODO: Я не совсем понимаю как правильно указать тут путь к этому файлу в University.API
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var assessmentServices = new AssessmentServices(new Mock<ILogger<AssessmentServices>>().Object, 
                new Mock<IHomeworkServices>().Object, Configuration, new List<INotification>(), 
                new Mock<IMarkAndVisitedRepository>().Object, new Mock<IStudentServices>().Object);
            StudentDto studentDto = new StudentDto
            {
                Name = "testStudent"
            };
            LectionDto lectionDto1 = new LectionDto
            {
                Name = "testLection1"
            };
            LectionDto lectionDto2 = new LectionDto
            {
                Name = "testLection2"
            };
            LectionDto lectionDto3 = new LectionDto
            {
                Name = "testLection3"
            };
            LectionDto lectionDto4 = new LectionDto
            {
                Name = "testLection4"
            };
            List<IStudentLectionVisitedDTO> studentLectionVisited = new List<IStudentLectionVisitedDTO>
            {
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto,
                    LectionDto = lectionDto1,
                    isVisited = true
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto,
                    LectionDto = lectionDto2,
                    isVisited = false
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto,
                    LectionDto = lectionDto3,
                    isVisited = false
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto,
                    LectionDto = lectionDto4,
                    isVisited = false
                }
            };
            var expectedResult = new {Students = studentDto.Name, Visit = 3};

            //act
           var result =  assessmentServices.CalculateStudentMissingByLector(studentLectionVisited);
           
           //assert
           Assert.That(result[0].Students, Is.EqualTo(expectedResult.Students));
            Assert.That(result[0].Visit, Is.EqualTo(expectedResult.Visit));
        }
        
        [Test]
        public void CalculateStudentMissingByLector_InputListThreeStudent_outputDynamic()
        {
            //arrange
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var assessmentServices = new AssessmentServices(new Mock<ILogger<AssessmentServices>>().Object, 
                new Mock<IHomeworkServices>().Object, Configuration, new List<INotification>(), 
                new Mock<IMarkAndVisitedRepository>().Object, new Mock<IStudentServices>().Object);
            StudentDto studentDto1 = new StudentDto
            {
                Name = "testStudent1"
            };
            StudentDto studentDto2 = new StudentDto
            {
                Name = "testStudent2"
            };
            StudentDto studentDto3 = new StudentDto
            {
                Name = "testStudent3"
            };
            LectionDto lectionDto1 = new LectionDto
            {
                Name = "testLection1"
            };
            LectionDto lectionDto2 = new LectionDto
            {
                Name = "testLection2"
            };
            LectionDto lectionDto3 = new LectionDto
            {
                Name = "testLection3"
            };
            LectionDto lectionDto4 = new LectionDto
            {
                Name = "testLection4"
            };
            List<IStudentLectionVisitedDTO> studentLectionVisited = new List<IStudentLectionVisitedDTO>
            {
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto1,
                    LectionDto = lectionDto1,
                    isVisited = true
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto1,
                    LectionDto = lectionDto2,
                    isVisited = false
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto1,
                    LectionDto = lectionDto3,
                    isVisited = false
                },
                new StudentLectionVisitedDto
                {
                    StudentDto = studentDto1,
                    LectionDto = lectionDto4,
                    isVisited = false
                },
                new StudentLectionVisitedDto
                {
                StudentDto = studentDto2,
                LectionDto = lectionDto1,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto2,
                LectionDto = lectionDto2,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto2,
                LectionDto = lectionDto3,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto2,
                LectionDto = lectionDto4,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto3,
                LectionDto = lectionDto1,
                isVisited = true
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto3,
                LectionDto = lectionDto2,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto3,
                LectionDto = lectionDto3,
                isVisited = false
            },
            new StudentLectionVisitedDto
            {
                StudentDto = studentDto3,
                LectionDto = lectionDto4,
                isVisited = true
            }
            };
            
            var expectedResult1 = new {Students = studentDto1.Name, Visit = 3 };
            var expectedResult2 = new {Students = studentDto2.Name, Visit = 3 };
            var expectedResult3 = new {Students = studentDto3.Name, Visit = 2 };
            //act
           var result =  assessmentServices.CalculateStudentMissingByLector(studentLectionVisited);
           
           //assert

            Assert.That(result[0].Students, Is.EqualTo(expectedResult1.Students));
            Assert.That(result[0].Visit, Is.EqualTo(expectedResult1.Visit));
            
            Assert.That(result[1].Students, Is.EqualTo(expectedResult2.Students));
            Assert.That(result[1].Visit, Is.EqualTo(expectedResult2.Visit));
            
            Assert.That(result[2].Students, Is.EqualTo(expectedResult3.Students));
            Assert.That(result[2].Visit, Is.EqualTo(expectedResult3.Visit));
        }
    }
}