using System.Linq;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class NonValidatedRecordsPersistenceService
    {
        private readonly IRepository<NonValidatedPartnershipRecord> _repository;
    

        public NonValidatedRecordsPersistenceService(IRepository<NonValidatedPartnershipRecord> repository)
        {
            _repository = repository;
        }


        public void Create(NonValidatedPartnershipRecord record)
        {
            _repository.Insert(record);
        }

        public void Delete(NonValidatedPartnershipRecord record)
        {
            NonValidatedPartnershipRecord single = GetSingle(record.Id);
            if (single != null)
            {
                _repository.Delete(single);
            }
        }

        public IQueryable<NonValidatedPartnershipRecord> GetAll()
        {
            return _repository.GetAll().AsQueryable();
        }

        public IQueryable<NonValidatedPartnershipRecord> GetForPartner(int id)
        {
            return
                _repository.Find(i => i.Partner == id)
                    .AsQueryable();
        }

        public IQueryable<NonValidatedPartnershipRecord> GetForPartner(string email, string phone)
        {
            return
                _repository.Find(i => i.Partner1.Email.Equals(email) || i.Partner1.Phone.Equals(phone)).AsQueryable();
        }

        public IQueryable<NonValidatedPartnershipRecord> GetForPartnershipArm(int partnershipArmId)
        {
            return
                _repository.Find(i => i.PartnershipArm == partnershipArmId).AsQueryable();
        }

        public NonValidatedPartnershipRecord GetSingle(int id)
        {
            return
                _repository.Find(i => i.Id == id)
                    .FirstOrDefault();
        }

       
    }
}