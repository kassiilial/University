using System;
using BusinessLogicInterfaces;

namespace University.BusinessLogic.Services.CommunicationServices
{
    public class SmsNotificationServices:INotification
    {
        public string NotificationType { get; set; }

        public SmsNotificationServices()
        {
            NotificationType = "SMS";
        }

        public void Notification(string name, string message)
        {
            Console.WriteLine($"SMS for {name}: {message}");
        }
    }
}