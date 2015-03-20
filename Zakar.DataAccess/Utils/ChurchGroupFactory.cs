using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class ChurchGroupFactory
    {
        public static Church BuildNew()
        {
            return new Church();
        }
    }
}