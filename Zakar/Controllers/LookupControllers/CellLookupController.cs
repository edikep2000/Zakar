using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using PagedList;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
    public class CellLookupController : Controller
    {
        private readonly CellService _cellService;

        public CellLookupController(CellService cellService)
        {
            _cellService = cellService;
        }

        public ActionResult GetItem(int? v)
        {
            var item = v.HasValue ? _cellService.GetSingle(v.Value) : new Cell();
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
                _cellService.GetAll()
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