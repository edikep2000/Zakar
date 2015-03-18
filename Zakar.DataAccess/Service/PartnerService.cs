using System;
using System.Data;
using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class PartnerService
    {
        private readonly IRepository<Partner> _repository;

        public PartnerService(IRepository<Partner> repository)
        {
            _repository = repository;
        }


        public void Create(Partner entity)
        {
            if ((GetPartnerByPhone(entity.Phone) != null) || (GetPartnerByEmail(entity.Email) != null))
            {
                throw new ConstraintException("Email or phone number already exists!");
            }
            _repository.Insert(entity);
        }

        public bool Exists(string email, string phone)
        {
            return !_repository.Find(o => o.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) || o.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase)).Any();
        }

        public IQueryable<Partner> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id descending
                select i);
        }

        public Partner GetPartnerByEmail(string email)
        {
            return _repository.Find(i => i.Email == email).FirstOrDefault();
        }

        public Partner GetPartnerByEmailAndPhone(string email, string phone)
        {
            return
                _repository.Find(
                    i =>
                        i.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase) &&
                        i.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public Partner GetPartnerByPhone(string phone)
        {
            return _repository.Find(i => i.Phone == phone).FirstOrDefault();
        }

        public Partner GetSingle(int id = 0)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }


        public IQueryable<Partner> Search(PartnerSearchModel model)
        {
            if (model == null)
            {
                return null;
            }
            return
                (from k in
                    _repository.Find(
                        i =>
                            (((i.FirstName.Contains(model.FirstNameSearchString) ||
                               i.LastName.Contains(model.LastNameSearchString)) ||
                              i.Email.Contains(model.EmailSearchString)) || i.Phone.Contains(model.PhoneNumberSearchString)) ||
                            i.YookosId.Contains(model.YookosIdSearchString))
                    orderby k.Id
                    select k).AsQueryable<Partner>();
        }

        public IQueryable<Partner> GetForChurch(int churchId)
        {
            return _repository.Find(i => i.ChurchId == churchId).AsQueryable();
        }
    }
}