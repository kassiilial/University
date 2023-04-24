using System;
using BusinessLogicInterfaces;

namespace University.BusinessLogic.Services.CommunicationServices
{
    public class EMailNotificationServices : INotification
    {
        public string NotificationType { get; set; }

        public EMailNotificationServices()
        {
            NotificationType = "Email";
        }

        public void Notification(string name, string message)
        {
            Console.WriteLine($"E-mail for {name}: {message}");
        }
    }
}