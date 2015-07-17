using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers.GridController
{
    public class GroupGridController : Controller
    {
        private readonly GroupService _groupService;

        public GroupGridController(GroupService groupService)
        {
            _groupService = groupService;
        }

        public ActionResult GroupRead(GridParams g, string search, int? zoneId)
        {
            search = (search ?? "").Trim();
            var m = _groupService.GetAll();
            m = zoneId.HasValue ? m.Where(i => i.ZoneId == zoneId) : m;
            var model = m.Select(i => new GroupListModel()
                {
                    Id = i.Id,
                    UniqueId = i.UniqueId,
                    Name = i.Name,
                    ZoneId = i.ZoneId,
                    ZoneName = i.Zone.Name
                });
             return Json(new GridModelBuilder<GroupListModel>(model, g)
                {
                    Key = "Id",
                    Map = o => new
                    {
                        o.Id,
                        o.Name,
                        o.UniqueId,
                        o.ZoneId,
                        o.ZoneName
                    }
                }.Build());
           
          
        }
    }
}