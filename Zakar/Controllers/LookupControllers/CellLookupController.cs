using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using PagedList;
using Zakar.Controllers.Extensions;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
      [Authorize]
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

        public ActionResult Search(string search, int page,int pcfId = 0)
        {
            search = (search ?? "").ToLower().Trim();
            var church = this.CurrentChurchAdministered().Result;
            if (pcfId == 0)
            {
                var list =
              _cellService.GetAll()
                  .Where(i => i.Name.Contains(search) &&i.PCF.ChurchId == church.Id)
                  .OrderByDescending(i => i.Id)
                  .ToPagedList(page, 10);
                return Json(new AjaxListResult
                {
                    More = list.HasNextPage,
                    Items = list.Select(i => new KeyContent(i.Id, i.Name))
                });
            }
            else
            {
                var list =
              _cellService.Find(i => i.PCFId == pcfId)
              .Where(i => i.Name.Contains(search) && i.PCF.ChurchId == church.Id)
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
}