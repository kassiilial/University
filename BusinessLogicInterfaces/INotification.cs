namespace BusinessLogicInterfaces
{
    public interface INotification
    {
        public string NotificationType { get; set; }
        public void Notification(string Name, string Message);
    }
}