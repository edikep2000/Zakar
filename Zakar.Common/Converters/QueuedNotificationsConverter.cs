using System;
using Zakar.Models;

namespace Zakar.Common.Converters
{
    public class QueuedNotificationsConverter
    {
        public static Notification Convert(QueuedNotification notification)
        {
            var notification2 = new Notification();
            if (notification != null)
            {
                notification2.DateSent = DateTime.Now;
                notification2.IsSent = true;
                notification2.Message = notification.Message;
                notification2.RecipientAddress = notification.RecipientAddress;
                notification2.NotificationCateoryCategoryId = notification.NotificationCateoryCategoryId;
            }
            return notification2;
        }
    }
}