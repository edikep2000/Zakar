using System;
using PagedList;

namespace Zakar.ViewModels
{
    public class QueuedNotificationSearchModel
    {
        public DateTime LastTriedEndDate { get; set; }

        public DateTime LastTriedStartDate { get; set; }

        public string MessageContains { get; set; }

        public string RecipientAddressSearch { get; set; }

        public int RetriesAttemptSearch { get; set; }

        public IPagedList<QueuedNotificationViewModel> SearchResults { get; set; }

        public DateTime SentEndDate { get; set; }

        public DateTime SentStartDate { get; set; }
    }
}