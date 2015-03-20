using System;
using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class QueuedNotificationFactory
    {
        public static QueuedNotification BuildNew()
        {
            return new QueuedNotification { DateToBeSent = DateTime.Now.AddHours(1.0), RetriesAttempt = 0 };
        }
    }
}