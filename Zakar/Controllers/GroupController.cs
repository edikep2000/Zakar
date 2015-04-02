using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class GroupController : ApiController
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            this._groupService = groupService;
        }

        public List<GroupViewModel> Get()
        {
            return (from i in this._groupService.GetAll()
                select new GroupViewModel { Id = i.Id, Name = i.Name } into i
                orderby i.Name
                select i).ToList<GroupViewModel>();
        }
    }
}