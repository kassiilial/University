//using DataTransferObjects;

namespace ForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            /*IUniversityServices createUniversityServices = new UniversityServices();
            IUniversityDTO universityDto=createUniversityServices.Create("ITMO");
            
            ILectorServices createLectorServices = new LectorServices();
            ILectorDTO lector1 = createLectorServices.Create(universityDto,"Lector1");
            ILectorDTO lector2 = createLectorServices.Create(universityDto,"Lector2");
            ILectorDTO lector3 = createLectorServices.Create(universityDto,"Lector3");
            
            ILectionServices createLectionServices = new LectionServices();
            ILectionDTO lection1 = createLectionServices.Create(lector1, "Lection1");
            ILectionDTO lection2 = createLectionServices.Create(lector1, "Lection2");
            ILectionDTO lection3 = createLectionServices.Create(lector1, "Lection3");
            ILectionDTO lection4 = createLectionServices.Create(lector1, "Lection4");
            //ILectionDTO lection2 = createLectionServices.Create(lector2, "Lection2");
            //ILectionDTO lection3 = createLectionServices.Create(lector3, "Lection3");
            
            IHomeworkServices createHomeworkServices = new HomeworkServices();
            IHomeworkDTO homeworkDto= createHomeworkServices.Create(lection1,"Homework1");
            IHomeworkDTO homework2= createHomeworkServices.Create(lection2,"Homework2");
            IHomeworkDTO homework3= createHomeworkServices.Create(lection3,"Homework3");
            IHomeworkDTO homework4= createHomeworkServices.Create(lection4,"Homework4");
            
            IStudentServices createStudentServices = new StudentServices();
            IStudentDTO student1 =createStudentServices.Create(universityDto,"Student1");
            IStudentDTO student2 =createStudentServices.Create(universityDto,"Student2");
            
            IAboutLectionVisit rate = new LectionVisitedServices();
            /*rate.SetLectionIsVisited(lection1, student1, true);
            rate.SetLectionIsVisited(lection1, student2, false);
            rate.SetLectionIsVisited(lection2, student1, false);
            rate.SetLectionIsVisited(lection2, student2, false);
            rate.SetLectionIsVisited(lection3, student1, true);
            rate.SetLectionIsVisited(lection3, student2, false);
            rate.SetLectionIsVisited(lection3, student2, false);
            rate.SetLectionIsVisited(lection3, student2, false);#1#
           // rate.SetHomeworkMarks(student1, homeworkDto, 5);
           // rate.SetHomeworkMarks(student2, homeworkDto, 3);
           rate.SetLectionIsVisited(lection1, student1, true);
           rate.SetLectionIsVisited(lection2, student1, true);
           rate.SetLectionIsVisited(lection3, student1, true);
           rate.SetLectionIsVisited(lection4, student1, true);
           rate.SetLectionIsVisited(lection1, student2, false);
           rate.SetLectionIsVisited(lection2, student2, false);
           rate.SetLectionIsVisited(lection3, student2, false);
           rate.SetLectionIsVisited(lection4, student2, false);
            rate.CheckVisited(lector1);*/
        }
    }
}