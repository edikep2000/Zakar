using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.LookupControllers
{
    public class GroupLookupController : Controller
    {
        private readonly GroupService _groupService;

        public GroupLookupController(GroupService groupService)
        {
            _groupService = groupService;
        }
    }
}