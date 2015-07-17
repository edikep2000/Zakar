using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using PagedList;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
      [Authorize]
    public class ChapterLookupController : Controller
    {
        private readonly ChurchService _churchService;

        public ChapterLookupController(ChurchService churchService)
        {
            _churchService = churchService;
        }

        public ActionResult GetItem(int? v)
        {
            var item = v.HasValue ? _churchService.GetSingle(v.Value) : new Church();
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
                _churchService.GetAll()
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