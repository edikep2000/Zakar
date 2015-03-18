using System;
using System.Collections.Generic;
using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class QueuedNotificationService
    {
        public IRepository<QueuedNotification> Repository { get; set; }

     

        public void Create(QueuedNotification notification)
        {
            if (notification != null)
            {
                Repository.Insert(notification);
            }
        }

        public void Create(IEnumerable<QueuedNotification> notification)
        {
            QueuedNotification[] source = (notification as QueuedNotification[]) ?? notification.ToArray();
            if ((notification != null) && source.Any())
            {
                foreach (QueuedNotification notification2 in source)
                {
                    Create(notification2);
                }
            }
        }

        public void Delete(int notificationId = 0)
        {
            QueuedNotification single = GetSingle(notificationId);
            if (single != null)
            {
                Repository.Delete(single);
            }
        }

        public IQueryable<QueuedNotification> GetAll()
        {
            return (from i in Repository.GetAll()
                orderby i.Id
                select i);
        }

        public IQueryable<QueuedNotification> GetAllSmsNotifications()
        {
            return (from i in GetAll()
                where i.NotificationCategory.Name.Equals("SMS", StringComparison.OrdinalIgnoreCase)
                orderby i.RetriesAttempt
                select i);
        }

        public IQueryable<QueuedNotification> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return (from i in GetAll()
                where (i.DateToBeSent > startDate) && (i.DateToBeSent <= endDate)
                orderby i.RetriesAttempt
                select i);
        }

        public QueuedNotification GetSingle(int notificationId = 0)
        {
            return
                Repository.Find(i => i.Id == notificationId)
                    .FirstOrDefault();
        }

        public IQueryable<QueuedNotification> Search(QueuedNotificationSearchModel model, int churchId = 0)
        {
            return
                (from i in
                    Repository.Find(
                        i =>
                            (((((i.DateToBeSent > model.SentStartDate) && (i.DateToBeSent < model.SentEndDate)) ||
                               (i.RetriesAttempt <= model.RetriesAttemptSearch)) ||
                              ((i.LastTried > model.LastTriedStartDate) && (i.LastTried <= model.LastTriedEndDate))) ||
                             i.Message.Contains(model.MessageContains)) ||
                            (i.RecipientAddress.Contains(model.RecipientAddressSearch) && (i.ChurchId == churchId)))
                    orderby i.Id
                    select i).AsQueryable<QueuedNotification>();
        }

      
    }
}