using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.LookupControllers
{
      [Authorize]
    public class PartnerLookUpController : Controller
    {
        private readonly PartnerService _partnerService;

        public PartnerLookUpController(PartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public ActionResult GetItem(int? v)
        {
            var item
                = v.HasValue && v.Value != 0 ? _partnerService.GetSingle(v.Value) : new Partner();
            if (item != null)
            {
                return Json(new KeyContent(item.Id, String.Format("{0} {1}", item.LastName, item.FirstName)));
            }
            return Json(new KeyContent(0, ""));
        }

        public PartialViewResult SearchForm()
        {
            return PartialView();
        }

        public ActionResult Search(int page, string search, int? zone, int? group, int? chapter)
        {
            const int pageSize = 10;
            search = (search ?? "").ToLower().Trim();
            var p = _partnerService.Find(i => i.FirstName.Contains(search) || i.LastName.Contains(search));
            p = zone.HasValue && zone.Value != 0 ? p.Where(i => i.Church.Group.ZoneId == zone) : p;
            p = group.HasValue && group.Value != 0 ? p.Where(i => i.Church.GroupId == group) : p;
            p = chapter.HasValue && chapter.Value != 0 ? p.Where(i => i.ChurchId == chapter) : p;
            p = p.OrderByDescending(i => i.Id);
            return Json(new AjaxListResult()
            {
                Items =
                    p.Skip((page - 1)*pageSize)
                        .Take(pageSize)
                        .Select(i => new KeyContent(i.Id, String.Format("{0} {1}", i.LastName, i.FirstName))),
                More = p.Count() > (page*pageSize)
            });
        }
    }
}