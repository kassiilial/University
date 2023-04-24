namespace Entities.Repositories.Exception
{
    public class ItemAlreadyExistsException:System.Exception
    {
        public ItemAlreadyExistsException(string message) : base(message)
        {
        }
    }
}