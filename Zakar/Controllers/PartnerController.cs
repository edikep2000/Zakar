using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.Common.FileHandlers;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class PartnerController : BaseController
    {
        private readonly PartnerService _partnerService;
        private readonly StagedPartnerService _stagedPartnerService;
        private readonly ChurchService _churchService;
        private readonly PartnerExcelFileHandler _fileHandler;
        public PartnerController(IUnitOfWork unitOfWork, ChurchService churchService, StagedPartnerService stagedPartnerService, PartnerService partnerService) : base(unitOfWork)
        {
            _churchService = churchService;
            _stagedPartnerService = stagedPartnerService;
            _partnerService = partnerService;
        }

        #region Partner Crud
        public ActionResult Index()
        {
            var church = GetChurchAdmin();
            if (church != null)
            {
                ViewBag.ModelExists = _partnerService.GetForChurch(church.Id).Any();
            }
            return View();
        }

        public PartialViewResult New()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult New(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<Partner>(model);
                var church = GetChurchAdmin();
                if (church != null)
                {
                    m.ChurchId = church.Id;
                    m.DateCreated = DateTime.Now;
                    m.UniqueId = Zakar.Common.IDGenerators.UniqueIdGenerator.GenerateUniqueIdForPartner(model.FullName);
                    _partnerService.Insert(m);
                    return Json(new { });
                }
                ModelState.AddModelError("", "Partner Must Belong to A Church");
            }
            return PartialView(model);
        }

        public ActionResult PartnerRead(GridParams g)
        {
            return Json(new { });
        }


        public PartialViewResult Edit(int id)
        {
            var m = _partnerService.GetSingle(id);
            var model = Mapper.Map<PartnerViewModel>(m);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = _partnerService.GetSingle(model.Id);
                if (m != null)
                {
                    m.CellId = model.CellId;
                    m.ChurchId = model.ChurchId;
                    m.DateCreated = model.DateCreated;
                    m.Email = model.Email;
                    m.FirstName = model.FirstName;
                    m.LastName = model.LastName;
                    m.PCFId = model.PCFId;
                    m.Phone = model.Phone;
                    m.Title = model.Title;
                    m.UniqueId = model.UniqueId;
                    return Json(new {});
                }
            }
            return PartialView();
        }

        public PartialViewResult Delete(int id, string gridId)
        {
            var p = _partnerService.GetSingle(id);
            if (p != null)
            {
                var model = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message =
                            String.Format("Are you sure you want to delete {0} and all attendant partnership logs?",
                                          p.LastName + " " + p.FirstName)
                    };
                return PartialView(model);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _partnerService.Delete(Convert.ToInt32(model.Id));
                return Json(new { });
            }
            return PartialView(model);
        }
        #endregion

        #region BulkUploadMethods
        public ActionResult Bulk()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Bulk(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                //something was actuall uploaded
                var stream = file.InputStream;
                try
                {
                    var c = GetChurchAdmin();
                    if (c != null)
                    {
                        _fileHandler.HandleFile(file.FileName, stream, c.Id);
                        return await Task.FromResult(RedirectToAction("Stage"));
                    }
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

        public ActionResult Stage()
        {
            var c = GetChurchAdmin();
            if (c != null)
            {
                ViewBag.ModelExists = _stagedPartnerService.Find(i => i.ChurchId == c.Id).Any();
            }
            return View();
        }

        public ActionResult StagePartnerRead(GridParams g)
        {
            return Json(new {});
        }

        public PartialViewResult StageCreate(int id)
        {
            var s = _stagedPartnerService.GetSingle(id);
            if (s != null)
            {
                var m = new PartnerViewModel()
                    {
                        CellId = s.CellId.HasValue ? s.CellId.Value : 0,
                        ChurchId = s.ChurchId.HasValue ? s.ChurchId.Value : 0,
                        DateCreated = s.DateCreated.HasValue ? s.DateCreated.Value : DateTime.Now,
                        Email = s.Email,
                        FirstName = s.FirstName,
                        Id = s.Id,
                        LastName = s.LastName,
                        PCFId = s.PCFId.HasValue ? s.PCFId.Value : 0,
                        Phone = s.Phone,
                        Title = s.Title,
                        UniqueId = s.UniqueId
                    };
                return PartialView(m);
            }
            return PartialView();
        }

        public ActionResult StageCreate(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<Partner>(model);
                _partnerService.Insert(m);
                return Json(new {});
            }
            return PartialView(model);
        }
        public PartialViewResult StageDelete(int id, string gridId)
        {
            var s = _stagedPartnerService.GetSingle(id);
            if (s != null)
            {
                var m = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message =
                            String.Format("Are you sure you want to delete the staged record for {0}",
                                          s.LastName + " " + s.FirstName)
                    };
                return PartialView("Delete", m);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult StageDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(model.Id);
                _stagedPartnerService.Delete(id);
                return Json(new {});
            }
            return PartialView("Delete", model);
        }
        #endregion

        #region HelperMethods
        private Church GetChurchAdmin()
        {
            var admin = User.Identity.GetUserId();
            var church = _churchService.GetChurchForAdmin(admin);
            return church;
        }
        #endregion
    }
}