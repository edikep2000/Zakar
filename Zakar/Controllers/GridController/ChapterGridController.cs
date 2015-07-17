using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers.GridController
{
    [Authorize]
    public class ChapterGridController : Controller
    {
        private readonly ChurchService _churchService;

        public ChapterGridController(ChurchService churchService)
        {
            _churchService = churchService;
        }

        public ActionResult ChapterRead(GridParams g, int? zoneId, int? groupId)
        {

            var i = _churchService.GetAll();
            i = zoneId.HasValue ? i.Where(j => j.Group.ZoneId == zoneId) : i;
            i = groupId.HasValue ? i.Where(j => j.GroupId == groupId) : i;
            var m = i.Select(j => new ChapterListModel
            {
                Id = j.Id,
                UniqueId = j.UniqueId,
                Name = j.Name,
                GroupId = j.GroupId,
                ZoneName = j.Group.Zone.Name,
                GroupName = j.Group.Name
            });
            return Json(new GridModelBuilder<ChapterListModel>(m, g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.Name,
                    o.UniqueId,
                    o.GroupId,
                    o.GroupName,
                    o.ZoneName
                }
            }.Build());
        }
    }
}