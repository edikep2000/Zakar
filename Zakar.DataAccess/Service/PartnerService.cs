using System;
using System.Data;
using System.Linq;
using Telerik.OpenAccess;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class PartnerService : Repository<Partner>
    {
        public PartnerService(IUnitOfWork unitOfWork): base(unitOfWork)
        {
            
        }
        public bool Exists(string email, string phone)
        {
            return Find(o => o.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase) || o.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase)).Any();
        }

        public Partner GetPartnerByEmail(string email)
        {
            return Find(i => i.Email == email).FirstOrDefault();
        }

        public Partner GetPartnerByEmailAndPhone(string email, string phone)
        {
            return
                Find(
                    i =>
                        i.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase) &&
                        i.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public Partner GetPartnerByPhone(string phone)
        {
            return Find(i => i.Phone == phone).FirstOrDefault();
        }

        public Partner GetSingle(int id = 0)
        {
            return Find(i => i.Id == id).FirstOrDefault();
        }

        public IQueryable<Partner> Search(PartnerSearchModel model)
        {
            if (model == null)
            {
                return null;
            }
            return
                (from k in
                    Find(
                        i =>
                            (((i.FirstName.Contains(model.FirstNameSearchString) ||
                               i.LastName.Contains(model.LastNameSearchString)) ||
                              i.Email.Contains(model.EmailSearchString)) || i.Phone.Contains(model.PhoneNumberSearchString)))
                    orderby k.Id
                    select k).AsQueryable<Partner>();
        }

        public IQueryable<Partner> GetForChurch(int churchId)
        {
            return Find(i => i.ChurchId == churchId).AsQueryable();
        }
    }
}