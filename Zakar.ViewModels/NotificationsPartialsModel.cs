using System;
using System.Collections.Generic;

namespace Zakar.ViewModels
{
    public class NotificationsPartialsModel
    {
        public NotificationsPartialsModel()
        {
            this.QueuedNotifications = new List<QueuedNotificationViewModel>();
        }

        public DateTime DateLastChecked { get; set; }

        public int NotificationsCount { get; set; }

        public IEnumerable<QueuedNotificationViewModel> QueuedNotifications { get; set; }
    }
}