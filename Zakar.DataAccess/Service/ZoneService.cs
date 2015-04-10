using System.Linq;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class ZoneService
    {
        private readonly IRepository<Zone> _repository;

        public ZoneService(IRepository<Zone> repository)
        {
            _repository = repository;
        }


        public void Create(Zone zone)
        {
            _repository.Insert(zone);
        }

        public Zone GetSingle(int id)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }

        public Zone GetSingle(string uniqueId)
        {
            return _repository.Find(i => i.UniqueId == uniqueId).FirstOrDefault();
        }

        public void Delete(Zone t)
        {
            _repository.Delete(t);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IQueryable<Zone> GetAll()
        {
            return _repository.GetAll();
        }
    }
}