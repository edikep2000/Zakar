using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class SetupController : BaseController
    {
        private readonly ChurchService _churchService;
        private readonly GroupService _groupService;
        private readonly ZoneService _zoneService;
        private readonly PartnershipArmService _armService;

        // GET: Zone
        public SetupController(IUnitOfWork unitOfWork, ChurchService churchService, GroupService groupService, ZoneService zoneService, PartnershipArmService armService) : base(unitOfWork)
        {
            _churchService = churchService;
            _groupService = groupService;
            _zoneService = zoneService;
            _armService = armService;
        }

        #region ZoneMethods
        public ActionResult ZoneIndex()
        {
            var recordExists = _zoneService.GetAll().Any();
            ViewBag.IsViewEmpty = recordExists;
            return View();

        }

        public ActionResult ZoneRead(GridParams g)
        {
            var model = _zoneService.GetAll().Select(i => new ZoneListModel
            {
                Id = i.Id,
                GroupCount = i.Groups.Count(),
                Name = i.Name
            });
            return Json(new GridModelBuilder<ZoneListModel>(model, g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.Name,
                    o.GroupCount
                }
            }.Build());
        }

        public PartialViewResult ZoneEdit(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ZoneEdit(ZoneViewModel model)
        {
            return PartialView(model);
        }


        public PartialViewResult Delete(int id, string gridId)
        {
            return PartialView();
        }


        [HttpPost]
        public PartialViewResult Delete(DeleteConfirmInput model)
        {
            return PartialView();
        }

        public PartialViewResult ZoneCreate()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ZoneCreate(ZoneViewModel model)
        {
            return Json(new {});
        }

        public PartialViewResult ZoneDetails(int id)
        {
            return PartialView();
        }

        #endregion
        #region GroupMethods

        public ActionResult GroupIndex()
        {
            return View();
        } 
        #endregion
        #region ChapterMethods
        public ActionResult ChapterIndex()
        {
            return View();
        } 
        #endregion
        #region PCFMethods
        public ActionResult PCFIndex()
        {
            return View();
        } 
        #endregion
        #region CellMethods
        public ActionResult CellIndex()
        {
            return View();
        } 
        #endregion
        #region ArmMethods

        public ActionResult ArmIndex()
        {
            return View();
        } 
        #endregion
        #region CurrencyMethods

        public ActionResult CurrencyIndex()
        {
            return View();
        } 
        #endregion
    }
}