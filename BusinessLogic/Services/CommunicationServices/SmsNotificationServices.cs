using System;
using BusinessLogicInterfaces;

namespace BusinessLogic.Services.Technical
{
    public class SmsNotificationServices:INotification
    {
        public string NotificationType { get; set; }

        public SmsNotificationServices()
        {
            NotificationType = "SMS";
        }

        public void Notification(string Name, string Message)
        {
            Console.WriteLine($"SMS Сообщение для {Name}: {Message}");
        }
    }
}