using System;

namespace Zakar.ViewModels
{
    public class QueuedNotificationViewModel
    {
        public DateTime DateToBeSent { get; set; }

        public int Id { get; set; }

        public DateTime LastTried { get; set; }

        public string Message { get; set; }

        public int NumberOfRetries { get; set; }

        public string RecipientAddress { get; set; }
    }
}