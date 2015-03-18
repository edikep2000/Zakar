using System;
using PagedList;

namespace Zakar.ViewModels
{
    public class NotificationSearchModel
    {
        public DateTime DateSentEnd { get; set; }

        public DateTime DateSentStart { get; set; }

        public bool IsSent { get; set; }

        public string MessageContains { get; set; }

        public string RecipientsAddress { get; set; }

        public IPagedList<NotificationViewModel> SearchResults { get; set; }
    }
}