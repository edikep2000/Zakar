using System.Linq;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class CurrencyService
    {
        private readonly IRepository<Currency> _repository;

        public CurrencyService(IRepository<Currency> repository)
        {
            _repository = repository;
        }


        public void Create(Currency entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IQueryable<Currency> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id
                select i).AsQueryable<Currency>();
        }

        public Currency GetCurrencyByName(string name)
        {
            return _repository.Find(i => i.Name == name).FirstOrDefault();
        }

        public Currency GetDefault()
        {
            return
                _repository.Find(i => i.IsDefaultCurrency)
                    .FirstOrDefault();
        }

        public Currency GetSingle(int id = 0)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }
    }
}