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
            return (from i in _repository.GetAll()
                    orderby i.ChurchId
                    select i);
        }

        public Church GetSingle(int id)
        {
            return _repository.Find(i => i.ChurchId == id).FirstOrDefault();
        }

        public Church GetSingle(string name)
        {
            return _repository.Find(i => i.Name == name).FirstOrDefault();
        }


        public IQueryable<Church> Search(ChurchSearchModel model)
        {
            return (from i in _repository.Find(i => i.Name.Contains(model.Name))
                    orderby i.ChurchId
                    select i).AsQueryable<Church>();
        }
    }
}
