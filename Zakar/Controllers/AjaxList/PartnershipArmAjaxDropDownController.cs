using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.AjaxList
{
    public class PartnershipArmAjaxDropDownController : Controller
    {
        private readonly PartnershipArmService _partnershipArmService;

        public PartnershipArmAjaxDropDownController(PartnershipArmService partnershipArmService)
        {
            _partnershipArmService = partnershipArmService;
        }


        public ActionResult GetItems(int? v)
        {
            var m = _partnershipArmService.GetAll().Select(i => new SelectableItem(i.Id, i.ShortFormName, i.Id == v)).ToList();
            return Json(m);
        }

    }
}