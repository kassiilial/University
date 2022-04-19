using System.Runtime.CompilerServices;

namespace BusinessLogic.Exception
{
    public class BusinesseLogicObjectNullException:System.Exception
    {
        public BusinesseLogicObjectNullException(string message) : base(message)
        {
        }
    }
}