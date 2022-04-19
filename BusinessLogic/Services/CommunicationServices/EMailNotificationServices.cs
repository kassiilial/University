using System;
using BusinessLogicInterfaces;

namespace BusinessLogic.Services.Technical
{
    public class EMailNotificationServices:INotification
    {
        public string NotificationType { get; set; }

        public EMailNotificationServices()
        {
            NotificationType = "Email";
        }

        public void Notification(string Name, string Message)
        {
            Console.WriteLine($"E-mail Сообщение для {Name}: {Message}");
        }
    }
}