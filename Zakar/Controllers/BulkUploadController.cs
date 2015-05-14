using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.Common.FileHandlers;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class BulkUploadController : BaseController
    {
        private readonly ZoneExcelFileHandler _zoneExcelFileHandler;
        private readonly GroupExcelFileHandler _groupExcelFileHandler;
        private readonly ChapterExcelFileHandler _chapterExcelFileHandler;
        private readonly StagedZoneService _stagedZoneService;
        private readonly StagedGroupService _stagedGroupService;
        private readonly StagedChurchService _stagedChurchService;
        private readonly ZoneService _zoneService;
        private readonly GroupService _groupService;
        private readonly ChurchService _churchService;


        public BulkUploadController(IUnitOfWork unitOfWork, ZoneExcelFileHandler zoneExcelFileHandler, GroupExcelFileHandler groupExcelFileHandler, ChapterExcelFileHandler chapterExcelFileHandler, StagedZoneService stagedZoneService, StagedGroupService stagedGroupService, StagedChurchService stagedChurchService, ZoneService zoneService, GroupService groupService, ChurchService churchService) : base(unitOfWork)
        {
            _zoneExcelFileHandler = zoneExcelFileHandler;
            _groupExcelFileHandler = groupExcelFileHandler;
            _chapterExcelFileHandler = chapterExcelFileHandler;
            _stagedZoneService = stagedZoneService;
            _stagedGroupService = stagedGroupService;
            _stagedChurchService = stagedChurchService;
            _zoneService = zoneService;
            _groupService = groupService;
            _churchService = churchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Zone
        public async Task<ActionResult> Zone()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<ActionResult> Zone(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                //something was actuall uploaded
                var stream = file.InputStream;
                try
                {
                    _zoneExcelFileHandler.HandleFile(file.FileName, stream);
                    return await Task.FromResult(RedirectToAction("ZoneIndex"));
                }
                catch
                {

                    ModelState.AddModelError("",
                                             "There was a problem uploading the file. It may either be corrupt or may not be a valid excel 2007-2003 file format. Please correct the problem and try again");

                }
            }
            else
            {
                ModelState.AddModelError("",
                                         "You Must upload something in the first place. Only Excel 2003-2007 file formats are however allowed");
            }
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> ZoneIndex()
        {
            ViewBag.ModelExists = _stagedZoneService.GetAll().Any();
            return await Task.FromResult(View());
        }

        public ActionResult ZoneRead(GridParams g, string search)
        {

            search = (search ?? "").ToLower();
            var item = _stagedZoneService.GetAll().Where(i => i.Name.Contains(search));
            return Json(new GridModelBuilder<StagedZone>(item.OrderByDescending(i => i.Id).AsQueryable(), g)
                {
                    Key = "Id",
                    GetItem = () => _stagedZoneService.GetSingle(Convert.ToInt32(g.Key))
                }.Build());
        } 

        public PartialViewResult ZoneCreate(int id)
        {
            var m = _stagedZoneService.GetSingle(id);
            var model = new ZoneViewModel()
                {
                    Name = m.Name,
                    UniqueId = m.UniqueId,
                    Id = m.Id
                };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ZoneCreate(ZoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mo = new Zone()
                    {
                        Name = model.Name,
                        UniqueId = model.UniqueId,
                    };
                _stagedZoneService.Delete(model.Id);
                _zoneService.Create(mo);
                return Json(new {});
            }
            ModelState.AddModelError("", "There are validation Errors");
            return PartialView(model);
        }

        public PartialViewResult ZoneDelete(int id, string gridId)
        {
            var m = _stagedZoneService.GetSingle(id);
            if (m != null)
            {
                var model = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message =
                            String.Format("Are you sure you want to delete the staged record for zone {0}", m.Name)
                    };
                return PartialView("Delete", model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ZoneDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _stagedZoneService.Delete(Convert.ToInt32(model.Id));
                return Json(new {});
            }
            return PartialView("Delete",model);
        }
        #endregion

        #region Group
        public ActionResult Group()
        {
            return View();
        }
                [HttpPost]
        public async Task<ActionResult> Group(HttpPostedFileBase file)
        {
             if (file != null && file.ContentLength > 0)
            {
                //something was actuall uploaded
                var stream = file.InputStream;
                try
                {
                    _groupExcelFileHandler.HandleFile(file.FileName, stream);
                    return await Task.FromResult(RedirectToAction("GroupIndex"));
                }
                catch
                {

                    ModelState.AddModelError("",
                                             "There was a problem uploading the file. It may either be corrupt or may not be a valid excel 2007-2003 file format. Please correct the problem and try again");

                }
            }
            else
            {
                ModelState.AddModelError("",
                                         "You Must upload something in the first place. Only Excel 2003-2007 file formats are however allowed");
            }
            return await Task.FromResult(View());
        }

        public ActionResult GroupIndex()
        {
            ViewBag.ModelExists = _stagedGroupService.GetAll().Any();
            return View();
        }

        public ActionResult GroupRead(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var item = _stagedGroupService.GetAll().Where(i => i.Name.Contains(search));
            return Json(new GridModelBuilder<StagedGroup>(item.OrderByDescending(i => i.Id).AsQueryable(), g)
            {
                Key = "Id",
                GetItem = () => _stagedGroupService.GetSingle(Convert.ToInt32(g.Key))
            }.Build());
        }

        public PartialViewResult GroupCreate(int id)
        {
            var m = _stagedGroupService.GetSingle(id);
            var model = new GroupViewModel
            {
                Name = m.Name,
                UniqueId = m.UniqueId,
                ZoneId = m.ZoneId,
                Id = m.Id
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult GroupCreate(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mo = new Group()
                {
                    Name = model.Name,
                    UniqueId = model.UniqueId,
                    ZoneId = model.ZoneId
                };
                _stagedGroupService.Delete(model.Id);
                _groupService.Create(mo);
                return Json(new { });
            }
            ModelState.AddModelError("", "There are validation Errors");
            return PartialView(model);
        }

        public PartialViewResult GroupDelete(int id, string gridId)
        {
            var m = _stagedGroupService.GetSingle(id);
            if (m != null)
            {
                var model = new DeleteConfirmInput()
                {
                    GridId = gridId,
                    Id = id,
                    Message =
                        String.Format("Are you sure you want to delete the staged record for zone {0}", m.Name)
                };
                return PartialView("Delete", model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult GroupDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _stagedGroupService.Delete(Convert.ToInt32(model.Id));
                return Json(new { });
            }
            return PartialView("Delete", model);
        }

        #endregion

        #region Chapter

        public ActionResult ChapterIndex()
        {
            ViewBag.ModelExists = _stagedChurchService.GetAll().Any();
            return View();
        }

        public ActionResult Chapter()
        {
            return View();
        }
                [HttpPost]
        public async Task<ActionResult> Chapter(HttpPostedFileBase file)
        {
             if (file != null && file.ContentLength > 0)
            {
                //something was actuall uploaded
                var stream = file.InputStream;
                try
                {
                    _chapterExcelFileHandler.HandleFile(file.FileName, stream);
                    return await Task.FromResult(RedirectToAction("ChapterIndex"));
                }
                catch
                {

                    ModelState.AddModelError("",
                                             "There was a problem uploading the file. It may either be corrupt or may not be a valid excel 2007-2003 file format. Please correct the problem and try again");

                }
            }
            else
            {
                ModelState.AddModelError("",
                                         "You Must upload something in the first place. Only Excel 2003-2007 file formats are however allowed");
            }
            return await Task.FromResult(View());
        }

        public ActionResult ChapterRead(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var item = _stagedChurchService.GetAll().Where(i => i.Name.Contains(search));
            return Json(new GridModelBuilder<StagedChurch>(item.OrderByDescending(i => i.Id).AsQueryable(), g)
            {
                Key = "Id",
                GetItem = () => _stagedChurchService.GetSingle(Convert.ToInt32(g.Key))
            }.Build());
        }

        public PartialViewResult ChapterCreate(int id)
        {
            var m = _stagedChurchService.GetSingle(id);
            var model = new ChurchViewModel
            {
                Name = m.Name,
                UniqueId = m.UniqueId,
                GroupId = m.GroupId,
                Id = m.Id
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChapterCreate(ChurchViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mo = new Church
                {
                    Name = model.Name,
                    UniqueId = model.UniqueId,
                    GroupId = model.GroupId,
                    DefaultCurrencyId = model.DefaultCurrencyId,
                };
                _stagedChurchService.Delete(model.Id);
                _churchService.Create(mo);
                return Json(new { });
            }
            ModelState.AddModelError("", "There are validation Errors");
            return PartialView(model);
        }

        public PartialViewResult ChapterDelete(int id, string gridId)
        {
            var m = _stagedChurchService.GetSingle(id);
            if (m != null)
            {
                var model = new DeleteConfirmInput()
                {
                    GridId = gridId,
                    Id = id,
                    Message =
                        String.Format("Are you sure you want to delete the staged record for zone {0}", m.Name)
                };
                return PartialView("Delete", model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChapterDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _stagedChurchService.Delete(Convert.ToInt32(model.Id));
                return Json(new { });
            }
            return PartialView("Delete", model);
        }
        #endregion
    }
}