using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;

namespace Zakar.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OpenAccessContext _context;

        public Repository(OpenAccessContext ctx)
        {
            _context = ctx;
        }
        public IQueryable<T> GetAll()
        {
            return _context.GetAll<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.GetAll<T>().Where(predicate);
        }

        public T GetSingle(Object id)
        {
            var objectKey = new ObjectKey(typeof(T).Name, id);
            var entity = _context.GetObjectByKey<T>(objectKey);
            return entity;
        }

        public void Delete(int id)
        {
            var t = GetSingle(id);
            if (t != null)
                Delete(t);
        }

        public void Delete(T t)
        {
            this._context.Delete(t);
        }

        public void Insert(T t)
        {
            if (t != null)
                _context.Add(t);
        }
    }
}