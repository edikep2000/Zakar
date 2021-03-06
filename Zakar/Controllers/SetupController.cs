﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.App_Start;
using Zakar.Common.Enums;
using Zakar.Controllers.Extensions;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    [Authorize]
    public class SetupController : BaseController
    {
        private readonly ChurchService _churchService;
        private readonly GroupService _groupService;
        private readonly ZoneService _zoneService;
        private readonly PartnershipArmService _armService;
        private readonly PCFService _pcfService;
        private readonly CellService _cellService;
        private readonly CurrencyService _currencyService;
        private readonly ApplicationUserManager _userManager;

        // GET: Zone
        public SetupController(IUnitOfWork unitOfWork, ChurchService churchService, GroupService groupService, ZoneService zoneService, PartnershipArmService armService, PCFService pcfService, CellService cellService, CurrencyService currencyService, ApplicationUserManager userManager)
            : base(unitOfWork)
        {
            _churchService = churchService;
            _groupService = groupService;
            _zoneService = zoneService;
            _armService = armService;
            _pcfService = pcfService;
            _cellService = cellService;
            _currencyService = currencyService;
            _userManager = userManager;
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
                GCount = i.Groups.Count(),
                UniqueId = i.UniqueId,
                Name = i.Name
            });
            return Json(new GridModelBuilder<ZoneListModel>(model, g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.Name,
                    o.GCount,
                    o.UniqueId
                }
            }.Build());
        }

        public PartialViewResult ZoneEdit(int id)
        {
            var model = _zoneService.GetSingle(id);
            var viewModel = Mapper.Map<ZoneViewModel>(model);
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult ZoneEdit(ZoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = _zoneService.GetSingle(model.Id);
                m.Name = model.Name;
                m.UniqueId = model.UniqueId;
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult ZoneDelete(int id, string gridId)
        {
            var zone = _zoneService.GetSingle(id);
            if (zone != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message = String.Format("Are you sure you want to delete Zone {0}, along with all the chapters, PCFs and Cells", zone.Name)
                    };
                return PartialView("Delete",m);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult ZoneDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _zoneService.Delete(Convert.ToInt32(model.Id));
                var user = _userManager.Users.FirstOrDefault(i => i.ZoneId == model.Id && i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.ZONE_ADMIN.ToString()));
                if (user != null)
                    _userManager.Delete(user);
                return Json(new {});
            }
            return PartialView();
        }

        public PartialViewResult ZoneCreate()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ZoneCreate(ZoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                ;
                var m = Mapper.Map<Zone>(model);
                _zoneService.Create(m);
                this.AccessContext.FlushChanges();
                m.UniqueId = "Z" + m.Id;
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult ZoneDetails(int id)
        {
            var model = _zoneService.GetSingle(id);
            if (model != null)
            {
                var m = new ZoneDetailsModel
                    {
                        GroupCount = model.Groups.Count(),
                        ChapterCount = _churchService.GetChurchInZone(model.Id).Count(),
                        Id = model.Id,
                        Name = model.Name,
                        PCFCount = 0,
                        PartnerCount = 0,
                        PartnershipTotal = 0
                    };
                return PartialView(m);
            }
            return PartialView();
        }

        #endregion
        #region GroupMethods

        public ActionResult GroupIndex()
        {
            var isModelExisting = _groupService.GetAll().Any();
            ViewBag.ModelExists = isModelExisting;
            return View();
        } 

   

        public PartialViewResult GroupCreate(int zoneId = 0)
        {
            if (zoneId != 0)
            {
                var
                model = new GroupViewModel()
                    {
                        ZoneId = zoneId
                    };
                return PartialView(model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult GroupCreate(GroupViewModel model)
        {

            if (ModelState.IsValid)
            {
                var group = Mapper.Map<Group>(model);
                if (User.IsInRole(RolesEnum.ZONE_ADMIN.ToString()))
                {
                    var zone = this.CurrentZoneAdministered().Result;
                    group.ZoneId = zone.Id;
                }
                _groupService.Create(group);
                AccessContext.FlushChanges();
                group.UniqueId = "G" + group.Id.ToString();
                return Json(new {});
            }
            return PartialView(model: model);
        }


        public PartialViewResult GroupEdit(int id = 0)
        {
            var group = _groupService.GetSingle(id);
            if (group != null)
            {
                var m = Mapper.Map<GroupViewModel>(group);
                return PartialView(m);
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult GroupEdit(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = _groupService.GetSingle(model.Id);
                if (m != null)
                {
                    m.Name = model.Name;
                    m.ZoneId = model.ZoneId;
                    return Json(new {});

                }
                ModelState.AddModelError("", "Cannot Edit a non existent Group");
            }
            return PartialView(model);
        }

        public PartialViewResult GroupDelete(int id, string gridId)
        {
            var group = _groupService.GetSingle(id);
            if (group != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message =
                            string.Format(
                                "Are you sure you want to delete group {0} along with all Churches, PCFs and Cells it contains",
                                group.Name)
                    };
                return PartialView("Delete",m);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult GroupDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _groupService.Delete(Convert.ToInt32(model.Id));
                var m =
                    _userManager.Users.FirstOrDefault(
                        i =>
                            i.GroupId == model.Id &&
                            i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.GROUP_ADMIN.ToString()));
                if (m != null)
                    _userManager.Delete(m);
                return Json(new {});
            }
            return PartialView("Delete",model);
        }

        public PartialViewResult GroupDetails(int id)
        {
            return PartialView();
        }
        #endregion
        #region ChapterMethods
        public ActionResult ChapterIndex()
        {
            var modelExists = _churchService.GetAll().Any();
            ViewBag.ModelExists = modelExists;
            return View();
        } 

      

        public PartialViewResult ChapterCreate(int groupId = 0)
        {
          

            if (User.IsInRole(RolesEnum.GROUP_ADMIN.ToString()))
            {
                var m = new ChurchViewModel
                {
                    GroupId = this.CurrentGroupAdministered().Result.Id
                };
                return PartialView(m);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChapterCreate(ChurchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<Church>(model);
                if (User.IsInRole(RolesEnum.GROUP_ADMIN.ToString()))
                {
                    var groupId = this.CurrentGroupAdministered().Result.Id;
                    m.GroupId = groupId;
                }
                _churchService.Create(m);
                this.AccessContext.FlushChanges();
                m.UniqueId = "C" + m.Id.ToString();
                return Json(new {});
            }
            return PartialView(model);
        }

        public ActionResult ChapterDetails(int id)
        {
            var chapter = _churchService.GetSingle(id);
            if (chapter != null)
                ViewBag.Title = String.Format("Partnership Summary For: {0}", chapter.Name);
            return View();
        }

        public PartialViewResult ChapterEdit(int id)
        {
            var m = _churchService.GetSingle(id);
            var model = Mapper.Map<ChurchViewModel>(m);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChapterEdit(ChurchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = _churchService.GetSingle(model.Id);
                if (m != null)
                {
                    m.DefaultCurrencyId = model.DefaultCurrencyId;
                    m.GroupId = model.GroupId;
                    m.Name = model.Name;
                    return Json(new {});
                }
                ModelState.AddModelError("", "Cannot update a non-existent record");
            }
            return PartialView();
        }

        public PartialViewResult ChapterDelete(int id, string gridId)
        {
            var model = _churchService.GetSingle(id);
            if (model != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message =
                            String.Format(
                                "Are you sure you want to delete {0} Along with Cells, PCFs and The Partner Information Contained",
                                model.Name)

                    };
                return PartialView("Delete", m);
            }
           
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult ChapterDelete(DeleteConfirmInput input)
        {
            if (ModelState.IsValid)
            {
                _churchService.Delete(Convert.ToInt32(input.Id));
                var m =
                    _userManager.Users.FirstOrDefault(
                        i => i.ChurchId == input.Id && i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.CHAPTER_ADMIN.ToString()));
                if (m != null)
                    _userManager.Delete(m);
                return Json(new {});
            }
            return PartialView("Delete");
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
            var modelExists = _armService.GetAll().Any();
            ViewBag.ModelExists = modelExists;
            return View();
        } 

        public ActionResult PartnershipArmRead(GridParams g)
        {
            var model = _armService.GetAll().Select(i => new PartnerhipArmListModel()
                {
                    Id = i.Id,
                    Description = i.Description,
                    Name = i.Name,
                    Partners = i.Partnerships.Count(),
                    ShortFormName = i.ShortFormName
                });
            return Json(new GridModelBuilder<PartnerhipArmListModel>(model, g)
                {
                    Key = "Id",
                    Map = o => new
                        {
                            o.Id,
                            o.Description,
                            o.Name,
                            o.Partners,
                            o.ShortFormName
                        }
                }.Build());
        }

        public PartialViewResult ArmCreate()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ArmCreate(PartnershipArmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<PartnershipArm>(model);
                _armService.Create(m);
                return Json(new {});
            }
            return PartialView(model);
        }


        public PartialViewResult ArmEdit(int id)
        {
            var m = _armService.GetSingle(id);
            if (m != null)
            {
                var model = Mapper.Map<PartnershipArmViewModel>(m);
                return PartialView(model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ArmEdit(PartnershipArmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var persistentEntity = _armService.GetSingle(model.Id);
                persistentEntity.Description = model.Description;
                persistentEntity.Name = model.Name;
                persistentEntity.ShortFormName = model.ShortFormName;
                return Json(new {});
            }
            return PartialView(model);
        }


        public PartialViewResult ArmDelete(int id, String gridId)
        {
            var model = _armService.GetSingle(id);
            if (model != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        Id = id,
                        GridId = gridId,
                        Message = String.Format("Are you sure you want to delete {0}", model.Name)
                    };
                return PartialView("Delete", m);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult ArmDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _armService.Delete(Convert.ToInt32(model.Id));
                return Json(new {});
            }
            return PartialView("Delete",model);
        }

        public PartialViewResult ArmDetails(int id)
        {
            return PartialView();
        }
        #endregion


        #region CurrencyMethods

        public ActionResult CurrencyIndex()
        {
            var modelExists = _currencyService.GetAll().Any();
            ViewBag.ModelExists = modelExists;
            return View();
        } 

        public ActionResult CurrencyRead(GridParams g)
        {
            var model = _currencyService.GetAll().Select(i => new CurrencyListModel()
                {
                    ConversionRateToDefault = i.ConversionRateToDefault,
                    Id = i.Id,
                    IsDefaultCurrency = i.IsDefaultCurrency,
                    Symbol = i.Symbol,
                    Name = i.Name,
                    PartnershipCount = i.Partnerships.Count()

                });
             return Json(new GridModelBuilder<CurrencyListModel>(model.OrderByDescending(i => i.Id).AsQueryable(), g)
            {
                Key = "Id",
                Map = o => new
                  {
                      o.ConversionRateToDefault,
                      o.Id,
                      o.IsDefaultCurrency,
                      o.Symbol,
                      o.Name,
                      o.PartnershipCount
                  }
            }.Build());
        }

        public PartialViewResult CurrencyCreate()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CurrencyCreate(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<Currency>(model);
                _currencyService.Create(m);
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult CurrencyEdit(int id)
        {
            var m = _currencyService.GetSingle(id);
            var viewModel = Mapper.Map<CurrencyViewModel>(m);
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult CurrencyEdit(CurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var persistentEntity = _currencyService.GetSingle(model.Id);
                if (persistentEntity != null)
                {
                    persistentEntity.ConversionRateToDefault = model.ConversionRateToDefault;
                    persistentEntity.IsDefaultCurrency = model.IsDefaultCurrency;
                    persistentEntity.Name = model.Name;
                    persistentEntity.Symbol = model.Symbol;
                    return Json(new { });
                }

                ModelState.AddModelError("", "Could not edit a record that does not exist");
            }
            return PartialView(model);
        }

        public PartialViewResult CurrencyDelete(int id, string gridId)
        {
            var model = _currencyService.GetSingle(id);
            if (model != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = model.Id,
                        Message =
                            String.Format("Are you sure you want to delete the currency {0} from the datastore",
                                          model.Symbol)
                    };
                return PartialView("Delete", m);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult CurrencyDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _currencyService.Delete(Convert.ToInt32(model.Id));
                return Json(new {});
            }
            return PartialView("Delete",model);
        }

        public ActionResult CurrencyDetails(int id)
        {
            var model = _currencyService.GetSingle(id);
            if (model != null)
            {
                //TODO Map a details for the currency
                return PartialView();
            }
            return PartialView();
        }
        #endregion
    }
}