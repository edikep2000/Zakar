using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using PagedList;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
    public class PartnershipArmLookupController : Controller
    {
        private readonly PartnershipArmService _armService;

        public PartnershipArmLookupController(PartnershipArmService armService)
        {
            _armService = armService;
        }

        public ActionResult GetItem(int? v)
        {
            var item = v.HasValue ? _armService.GetSingle(v.Value) : new PartnershipArm();
            if (item != null)
            {
                return Json(new KeyContent(item.Id, item.Name));
            }
            return Json(new KeyContent(0, ""));
        }

        public ActionResult Search(string search, int page)
        {
            search = (search ?? "").ToLower().Trim();
            var list =
                _armService.GetAll()
                    .Where(i => i.Name.Contains(search))
                    .OrderByDescending(i => i.Id)
                    .ToPagedList(page, 10);
            return Json(new AjaxListResult
            {
                More = list.HasNextPage,
                Items = list.Select(i => new KeyContent(i.Id, i.Name))
            });

        }
    }
}