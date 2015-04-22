using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class ChurchService
    {

        private readonly IRepository<Church> _repository;

        public ChurchService(IRepository<Church> repository)
        {
            _repository = repository;
        }

        public void Create(Church entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }

        public IQueryable<Church> GetAll()
        {
            return _repository.GetAll();
        }

        public IQueryable<Church> GetChurchInGroup(int groupId)
        {
            var m = _repository.Find(i => i.GroupId == groupId);
            return m;
        }

        public IQueryable<Church> GetChurchInZone(int zoneId)
        {
            var model = _repository.Find(i => i.Group.ZoneId == zoneId);
            return model;
        }

        public Church GetSingle(int id)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }

        public Church GetSingle(string name)
        {
            return _repository.Find(i => i.Name == name).FirstOrDefault();
        }

        public IQueryable<Church> Search(ChurchSearchModel model)
        {
            return (from i in _repository.Find(i => i.Name.Contains(model.Name))
                    orderby i.Id
                    select i).AsQueryable<Church>();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Church GetChurchForAdmin(string userId)
        {
            return _repository.Find(i => i.AdminId.Equals(userId)).FirstOrDefault();
        }
    }
}
