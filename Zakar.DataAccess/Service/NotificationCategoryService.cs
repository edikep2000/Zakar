using System.Linq;
using System.Linq.Expressions;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class NotificationCategoryService
    {
        private readonly IRepository<NotificationCategory> _repository;

        public NotificationCategoryService(IRepository<NotificationCategory> repository)
        {
            _repository = repository;
        }


        public void Create(NotificationCategory category)
        {
            if (category != null)
            {
                _repository.Insert(category);
            }
        }

        public IQueryable<NotificationCategory> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.CategoryId
                select i);
        }

        public NotificationCategory GetSingle(int Id = 0)
        {
            return
                _repository.Find(i => i.CategoryId == Id)
                    .FirstOrDefault();
        }

    }


    public class NotificationService
    {
        private readonly IRepository<Notification> _repository;

        public NotificationService(IRepository<Notification> repository)
        {
            _repository = repository;
        }

        public void Create(Notification item)
        {
            if (item != null)
            {
                _repository.Insert(item);
            }
        }

        public void Delete(int Id)
        {
            _repository.Delete(GetSingle(Id));
        }

        public IQueryable<Notification> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Notification> GetSearch(NotificationSearchModel model, int churchId)
        {
            return
                (from i in
                     _repository.Find(
                         i =>
                         (((i.DateSent > model.DateSentStart) && (i.DateSent <= model.DateSentEnd)) ||
                          i.Message.Contains(model.MessageContains)) ||
                         (i.RecipientAddress.Contains(model.RecipientsAddress) && (i.ChurchId == churchId)))
                 orderby i.Id
                 select i).AsQueryable<Notification>();
        }

        public Notification GetSingle(int id)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }

     

    }
}