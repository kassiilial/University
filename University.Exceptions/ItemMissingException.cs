namespace Entities.Repositories.Exception
{
    public class ItemMissingException:System.Exception
    {
        public ItemMissingException(string message) : base(message)
        {
            
        }
    }
}