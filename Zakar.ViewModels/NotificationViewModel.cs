using System;

namespace Zakar.ViewModels
{
    public class NotificationViewModel
    {
        public DateTime DateSent { get; set; }

        public int Id { get; set; }

        public bool IsSent { get; set; }

        public string Message { get; set; }

        public string RecipientAddress { get; set; }
    }
}