using Zakar.Models;

namespace Zakar.Common.Events.EventConsumers
{
    public class QueuedNotificationEventArgs : System.EventArgs
    {
        private readonly QueuedNotification _notification;

        public QueuedNotificationEventArgs(QueuedNotification q)
        {
            this._notification = q;
        }

        public QueuedNotification Notification
        {
            get
            {
                return this._notification;
            }
        }
    }
}