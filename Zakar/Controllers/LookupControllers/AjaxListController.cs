﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using OpenAccessRuntime.util.classhelper;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.LookupControllers
{
      [Authorize]
    public class AjaxListController : Controller
    {
        private readonly ZoneService _zoneService;
        private readonly GroupService _groupService;
        private readonly ChurchService _churchService;
        private readonly PartnershipArmService _armService;
        private readonly CurrencyService _currencyService;
        private readonly CellService _cellService;
        private readonly PCFService _pcfService;

        public AjaxListController(ZoneService zoneService, GroupService groupService, ChurchService churchService, PartnershipArmService armService, CurrencyService currencyService, PCFService pcfService, CellService cellService)
        {
            _zoneService = zoneService;
            _groupService = groupService;
            _churchService = churchService;
            _armService = armService;
            _currencyService = currencyService;
            _pcfService = pcfService;
            _cellService = cellService;
        }
        public ActionResult GetZones(int? v)
        {
            var model = _zoneService.GetAll().Select(i => new SelectableItem(i.Id, i.Name, v == i.Id)).AsEnumerable();
            return Json(model);
        }

        public ActionResult GetCurrencies(int? v)
        {
            var model =
                _currencyService.GetAll().Select(i => new SelectableItem(i.Id, i.Name, v == i.Id)).AsEnumerable();
            return Json(model);
        }

        public ActionResult GetGroups(int? v, int? zoneId)
        {
           
                var model = _groupService.GetAll();
                model = zoneId.HasValue ? model.Where(i => i.ZoneId == zoneId) : model;
                var m = model.Select(i => new SelectableItem(i.Id, i.Name, v == i.Id)).AsEnumerable();
                return Json(m);
           
        }

        public ActionResult GetChurches(int? v, int? zoneId, int? groupId)
        {
            var model = _churchService.GetAll();

            model = zoneId.HasValue ? model.Where(i => i.Group.ZoneId == zoneId) : model;

            model = groupId.HasValue ? model.Where(i => i.GroupId == groupId) : model;

            var resultModel = model.Select(i => new SelectableItem()
                {
                    Text = i.Name,
                    Value = i.Id,
                    Selected = i.Id == v
                });
            return Json(resultModel);
        }

        public ActionResult GetPCFs(int? v, int? zoneId, int? groupId, int? churchId)
        {
            var model = _pcfService.GetAll();
            model = zoneId.HasValue ? model.Where(i => i.Church.Group.ZoneId == zoneId) : model;
            model = groupId.HasValue ? model.Where(i => i.Church.GroupId == groupId) : model;
            model = churchId.HasValue ? model.Where(i => i.ChurchId == churchId) : model;
            var returnValue = model.Select(i => new SelectableItem()
                {
                    Value = i.Id,
                    Text = i.Name,
                    Selected = i.Id == v
                });
            return Json(returnValue);
        }

        public ActionResult GetTitles(string v)
        {
            var model = new List<string>()
                {
                    "Brother",
                    "Sister",
                    "Pastor",
                    "Deacon",
                    "Deaconess",
                    "Reverend",
                    "Evangelist"
                };
            var returnValue = model.Select(i => new SelectableItem()
                {
                    Selected = v == i,
                    Text = i,
                    Value = i
                });
            return Json(returnValue);
        }

        public ActionResult GetGender(string v)
        {
            var list = new List<string>
                {
                    "Male",
                    "Female"
                };
            var model = list.Select(i => new SelectableItem()
                {
                    Selected = i == v,
                    Text = i,
                    Value = i
                });
            return Json(model);
        }

        public ActionResult GetCells(int? v, int pcfId = 0, int churchId = 0)
        {
            var model = _cellService.GetAll();
            if (pcfId != 0)
                model = model.Where(i => i.PCFId == pcfId);
            if (churchId != 0)
                model = model.Where(i => i.PCF.ChurchId == churchId);
            var returnValue = model.Select(i => new SelectableItem()
                {
                    Value = i.Id,
                    Text = i.Name,
                    Selected = i.Id == v
                }).AsEnumerable();
            return Json(returnValue);
        }

	}
}