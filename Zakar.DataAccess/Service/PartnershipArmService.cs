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
               
                _repository.Insert(entity);
            }
        }

        public void Delete(int id)
        {
            PartnershipArm single = GetSingle(id);
            if (single != null)
            {
                _repository.Delete(single);
            }
        }

        public IQueryable<PartnershipArm> GetAll()
        {
            return _repository.GetAll();
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
                            i.ShortFormName.Equals(shortName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }
            return null;
        }

     
    }
}