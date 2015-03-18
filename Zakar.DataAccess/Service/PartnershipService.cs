using System;
using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class PartnershipService
    {
        private CurrencyService _currencyService;

       
        public PartnershipService(CurrencyService service, IRepository<Partnership> repository)
        {
            _currencyService = service;
            _repository = repository;
        }


        private readonly IRepository<Partnership> _repository;


        public void Create(Partnership entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }

        public void Delete(int id)
        {
            Partnership single = GetSingle(id);
            if (single != null)
            {
                _repository.Delete(single);
            }
        }

        public IQueryable<Partnership> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id descending
                select i);
        }

        public IQueryable<Partnership> GetByEmailAndPhone(string email, string phone)
        {
            return
                _repository.Find(
                    i =>
                        i.Partner.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) &&
                        i.Partner.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
        }

        public IQueryable<Partnership> GetByYear(int year)
        {
            return
                (from i in
                    _repository.Find(I => I.Year == year).AsQueryable()
                    orderby i.Id
                    select i);
        }

        public IQueryable<Partnership> GetPartnershipRecordsForPartner(int partnerid)
        {
            return
                (from i in
                    _repository.Find(i => i.PartnerId == partnerid)
                        .AsQueryable()
                    orderby i.Id
                    select i);
        }

        public Partnership GetSingle(int key = 0)
        {
            return _repository.Find(i => i.Id == key).FirstOrDefault();
        }


        public IQueryable<PartnershipResultModel> Search(PartnershipSearchModel model)
        {
            return (from o in _repository.GetAll()
                where
                    ((((((o.CurrencyId == model.CurrencyId) && (o.Month == model.Month)) && (o.Year == model.Year)) &&
                       (o.PartnershipArmId == model.PartnershipArmId)) ||
                      o.Partner.FirstName.Contains(model.FirstName)) || o.Partner.LastName.Contains(model.LastName)) ||
                    (o.Partner.Phone == ("234" + model.PhoneNumber))
                select
                    new PartnershipResultModel
                    {
                        PartnerId = o.PartnerId,
                        PartnershipId = o.Id,
                        PartnershipArm = o.PartnershipArm.Name,
                        CurrencySymbolId = o.CurrencyId,
                        CurrencySymbol = o.Currency.Symbol,
                        Amount = o.Amount,
                        PartnersFullName = (o.Partner.Title + " " + o.Partner.LastName) + " " + o.Partner.FirstName,
                        Year = o.Year,
                        Month = o.Month,
                        PartnershipArmId = o.PartnershipArmId
                    }
                into i
                orderby i.PartnershipId
                select i);
        }

        public IQueryable<PartnershipResultModel> Search(PartnershipSearchModel model, int churchId)
        {
            return (from o in _repository.GetAll()
                where
                    (((((((o.CurrencyId == model.CurrencyId) && (o.Month == model.Month)) && (o.Year == model.Year)) &&
                        (o.PartnershipArmId == model.PartnershipArmId)) && (o.Partner.ChurchId == churchId)) ||
                      o.Partner.FirstName.Contains(model.FirstName)) || o.Partner.LastName.Contains(model.LastName)) ||
                    (o.Partner.Phone == ("234" + model.PhoneNumber))
                select
                    new PartnershipResultModel
                    {
                        PartnerId = o.PartnerId,
                        PartnershipId = o.Id,
                        PartnershipArm = o.PartnershipArm.Name,
                        CurrencySymbolId = o.CurrencyId,
                        CurrencySymbol = o.Currency.Symbol,
                        Amount = o.Amount,
                        PartnersFullName = (o.Partner.Title + " " + o.Partner.LastName) + " " + o.Partner.FirstName,
                        Year = o.Year,
                        Month = o.Month,
                        PartnershipArmId = o.PartnershipArmId
                    }
                into i
                orderby i.PartnershipId
                select i);
        }

        public IQueryable<Partnership> GetForChurch(int churchId)
        {
            IQueryable<Partnership> returnValue = _repository.Find(i => i.Partner.ChurchId == churchId).AsQueryable();
            return returnValue;
        }
    }
}