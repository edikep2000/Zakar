using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class NotificationsCategoryFactory
    {
        public static NotificationCategory BuildNewCategory()
        {
            return new NotificationCategory();
        }
    }
}