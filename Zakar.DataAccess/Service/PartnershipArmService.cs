using System;
using System.Linq;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class PartnershipArmService
    {
        private readonly IRepository<PartnershipArm> _repository;

        public PartnershipArmService(IRepository<PartnershipArm> repository)
        {
            _repository = repository;
        }


        public void Create(PartnershipArm entity)
        {
            if (entity != null)
            {
                entity.Deleted = false;
                _repository.Insert(entity);
            }
        }

        public bool Delete(int id)
        {
            PartnershipArm single = GetSingle(id);
            if (single != null)
            {
                single.DateDeleted = DateTime.Now;
                single.Deleted = true;
                return true;
            }
            return false;
        }

        public IQueryable<PartnershipArm> GetAll()
        {
            return (from I in
                (from i in _repository.GetAll()
                    where !i.Deleted
                    select i).AsQueryable<PartnershipArm>()
                orderby I.Id
                select I);
        }

        public PartnershipArm GetSingle(int id = 0)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }

        public PartnershipArm GetSingle(string shortName)
        {
            if (!string.IsNullOrEmpty(shortName))
            {
                return
                    _repository.Find(
                        i =>
                            i.ShortFormName.Equals(shortName, StringComparison.InvariantCultureIgnoreCase) && !i.Deleted).FirstOrDefault();
            }
            return null;
        }

     
    }
}