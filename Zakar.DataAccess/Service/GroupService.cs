using System.Linq;
using Telerik.OpenAccess.Util;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class GroupService
    {
        private readonly IRepository<Group> _repository;

        public GroupService(IRepository<Group> repository)
        {
            _repository = repository;
        }


        public void Create(Group group)
        {
            if (group != null)
            {
                _repository.Insert(group);
            }
        }

        public IQueryable<Group> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id
                select i);
        }

        public Group GetSingle(int id = 0)
        {
            return _repository.Find(i => i.Id == id).FirstOrDefault();
        }

    }
}