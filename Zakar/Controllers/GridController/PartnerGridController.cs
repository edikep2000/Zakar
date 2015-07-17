using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Zakar.Common.Enums;
using Zakar.Controllers.Extensions;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers.GridController
{
    public class PartnerGridController : Controller
    {
        private readonly PartnerService _partnerService;

        public PartnerGridController(PartnerService partnerService)
        {
            _partnerService = partnerService;
        }


        public  ActionResult PartnerRead(GridParams g, string search, int? churchId , int? groupId , int? zoneId)
        {
            search = (search ?? "").Trim();
            var query = _partnerService.GetAll().Where(i => i.FirstName.Contains(search) || i.LastName.Contains(search));
            query = zoneId.HasValue ? _partnerService.Find(i => i.Church.Group.ZoneId == zoneId) : query;
            query = groupId.HasValue ? _partnerService.Find(i => i.Church.GroupId == groupId) : query;
            var model = query.Select(i => new PartnerListModel
            {
                Id = i.Id,
                ChurchId = i.ChurchId,
                ChurchName = i.Church.Name,
                DateCreated = i.DateCreated,
                DateOfBirth = i.DateOfBirth,
                Email = i.Email,
                FullName = i.LastName + " " + i.FirstName,
                Gender = i.Gender,
                Phone = i.Phone,
                Title = i.Title,
                UniqueId = i.UniqueId,
                GroupName = i.Church.Group.Name,
                ZoneName = i.Church.Group.Zone.Name
            });
            return Json(new GridModelBuilder<PartnerListModel>(model.OrderByDescending(I => I.DateCreated), g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.ChurchId,
                    o.ChurchName,
                    o.DateCreated,
                    o.DateOfBirth,
                    o.Email,
                    o.FullName,
                    o.Gender,
                    o.Phone,
                    o.Title,
                    o.UniqueId,
                    o.GroupName,
                    o.ZoneName,
                }
            }.Build());
        }
    }
}