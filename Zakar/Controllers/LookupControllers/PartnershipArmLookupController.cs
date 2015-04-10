using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.LookupControllers
{
    public class PartnershipArmLookupController : Controller
    {
        private readonly PartnershipArmService _armService;

        public PartnershipArmLookupController(PartnershipArmService armService)
        {
            _armService = armService;
        }
    }
}