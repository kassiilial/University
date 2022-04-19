using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BusinessLogicInterfaces;
using DataTransferObject;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class VisitReport:IVisitReport
    {
        private readonly ILogger<VisitReport> _logger;
        private readonly IStudentServices _studentServices;
        private readonly ILectionServices _lectionServices;
        
        public VisitReport( [NotNull]ILogger<VisitReport> logger,
            [NotNull]IStudentServices studentServices, [NotNull]ILectionServices lectionServices)
        {
            _logger = logger;
            _studentServices = studentServices;
            _lectionServices = lectionServices;
        }
        
        public string GenerateSerialisedVisitReport([NotNull]object obj, MySerializator format)
        {
            Object report = MakeReportByType(obj);
            string result = SerialiseReport(report, format);
            _logger.LogInformation("Visited report for WEB");
            return result;
        }

        internal object MakeReportByType(object inputObj)
        {
            object outputObject = new object();
            if (inputObj is IStudentDTO forStudentReport)
            {
                var studentLectionVisitedDtos=_studentServices.GetMarkAndVisitedByStudent(forStudentReport);
                StudentReport report = new StudentReport();
                report.MakeReport(studentLectionVisitedDtos.ToList());
                outputObject = report;
            }
            else
            {
                if (inputObj is ILectionDTO forLectionReport)
                {
                    var studentLectionVisitedDtos=_lectionServices.GetMarkAndVisitedByLection(forLectionReport);
                    LectionReport report = new LectionReport();
                    report.MakeReport(studentLectionVisitedDtos.ToList());
                    outputObject = report;
                }
                else
                {
                    throw new ArgumentException("Not correct type for report");
                }
            }
            return outputObject;
        }

        internal string SerialiseReport(object report, MySerializator format)
        {
            ISerializatorFactory serializatorFactory = new SerializatorFactory();
            ISerializator serializator = serializatorFactory.MakeChoiceOfFormat(format);
            try
            {
                return serializator.Serialize(report);
            }
            catch (System.Exception)
            {
                throw new ArgumentNullException("Seralization is fall");
            }
        }
    }
}