using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class NotificationFactory
    {
        public static Notification BuildNew()
        {
            return new Notification();
        }
    }
}