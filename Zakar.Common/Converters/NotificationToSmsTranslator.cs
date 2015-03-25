using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Common.Converters
{
    public class NotificationToSmsTranslator
    {
        public static SMSMessage BuildMessageFromNotification(QueuedNotification notification)
        {
            SMSMessage message = null;
            if (notification != null)
            {
                message = new SMSMessage
                {
                    Body = notification.Message,
                    Recipient = notification.RecipientAddress
                };
            }
            return message;
        }
    }
}
