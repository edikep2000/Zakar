using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using PagedList;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
      [Authorize]
    public class ZoneLookupController : Controller
    {
        private readonly ZoneService _zoneService;

        public ZoneLookupController(ZoneService zoneService)
        {
            _zoneService = zoneService;
        }
        public ActionResult GetItem(int? v)
        {
            var item = v.HasValue ? _zoneService.GetSingle(v.Value) : new Zone();
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
                _zoneService.GetAll()
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