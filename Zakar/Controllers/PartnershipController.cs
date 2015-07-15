using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNet.Identity;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.Common.FileHandlers;
using Zakar.Controllers.Extensions;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    [Authorize]
    public class PartnershipController : BaseController
    {
        private readonly PartnershipService _partnershipService;
        private readonly StagedPartnershipService _stagedPartnershipService;
        private readonly ChurchService _churchService;
        private readonly PartnershipLogExcelFileHandler _fileHandler;
        //
        // GET: /Partnership/
        public PartnershipController(IUnitOfWork unitOfWork, PartnershipService partnershipService, StagedPartnershipService stagedPartnershipService, ChurchService churchService, PartnershipLogExcelFileHandler fileHandler) : base(unitOfWork)
        {
            _partnershipService = partnershipService;
            _stagedPartnershipService = stagedPartnershipService;
            _churchService = churchService;
            _fileHandler = fileHandler;
        }

        #region Crud Methods

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult New()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult New(PartnershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var p = new Zakar.Models.Partnership
                {
                    Amount = model.Amount,
                    CurrencyId = model.CurrencyId,
                    DateCreated = DateTime.Now,
                    Month = model.Month,
                    PartnerId = model.PartnerId,
                    PartnershipArmId = model.PartnershipArmId,
                    Source = "WEB",
                    Year = model.Year
                };
                _partnershipService.Create(p);
                AccessContext.FlushChanges();
                p.TrackingId = "PO-" + p.Id.ToString();
                TempData["Message"] = "Partnership Record Logged Successfully";
                return RedirectToAction("New");
            }
            return View(model);
        }

        public PartialViewResult Edit(int id = 0)
        {
            var p = _partnershipService.GetSingle(id);
            var model = Mapper.Map<PartnershipViewModel>(p);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(PartnershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var p = _partnershipService.GetSingle(model.Id);
                if (p != null)
                {
                    p.Amount = model.Amount;
                    p.CurrencyId = model.CurrencyId;
                    p.Month = model.Month;
                    p.PartnerId = model.PartnerId;
                    p.PartnershipArmId = model.PartnershipArmId;
                    p.Year = model.Year;
                    return Json(new {});
                }
                ModelState.AddModelError("","Cannot update a non-existent entity");
            }
            return PartialView(model);
        }

        public PartialViewResult Delete(int id, string gridId)
        {
            var p = _partnershipService.GetSingle(id);
            if (p != null)
            {
                var m = new DeleteConfirmInput()
                {
                    GridId = gridId,
                    Id = id,
                    Message =
                        String.Format("Are you sure you want to delete the partnership record with Id", p.TrackingId)
                };
                return PartialView(m);
            }
            return PartialView();
        }

        public ActionResult Delete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _partnershipService.Delete(model.Id);
                return Json(new {});
            }
            return PartialView(model);
        } 
        #endregion

        #region Bulk Methods

        public ActionResult Bulk()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Bulk(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var stream = file.InputStream;
                try
                {
                    var church =  await this.CurrentChurchAdministered();
                    if (church != null)
                    {
                       _fileHandler.HandleFile(file.FileName, stream, church.Id);
                        return await Task.FromResult(RedirectToAction("Stage"));
                    }

                }
                catch
                {
                    ModelState.AddModelError("",
                        "There was a problem uploading the file, it may either be corrupt or may not be a valid excel 2003-2007 file format. Please correct the problem and try again");
                }
            }
            else
            {
                ModelState.AddModelError("",
                                         "You Must upload something in the first place. Only Excel 2003-2007 file formats are however allowed");
           
            }

            return await Task.FromResult(View());
        }



        public async Task<ActionResult> Stage()
        {
            var c = await this.CurrentChurchAdministered();
            int churchId = 0;
            if (c != null)
                churchId = c.Id;
            ViewBag.ModelExists = _stagedPartnershipService.Find(i => i.ChurchId == churchId);
          return View();
         }

        public async Task<ActionResult> StageRead(GridParams g, string search)
        {
            search = (search ?? "").Trim();
            var church = await this.CurrentChurchAdministered();
            var m = _stagedPartnershipService.Find(i => i.ChurchId == church.Id && i.PartnerId.HasValue);
            return Json(new GridModelBuilder<StagedPartnership>(m.OrderBy(i => i.Id).AsQueryable(), g)
            {
                Key = "Id",
                GetItem = () => _stagedPartnershipService.GetSingle(Convert.ToInt32(g.Key))
            }.Build());
        }

        public ActionResult StageCreate(int id)
        {
            var s = _stagedPartnershipService.GetSingle(id);
            if (s != null)
            {
                var p = new PartnershipViewModel
                {
                    Amount = s.Amount,
                    CurrencyId = s.CurrencyId.HasValue ? s.CurrencyId.Value : 0,
                    Month = s.Month.HasValue ? s.Month.Value : 0,
                    PartnerId = s.PartnerId.HasValue ? s.PartnerId.Value : 0,
                    PartnershipArmId = s.ArmId.HasValue ? s.ArmId.Value : 0,
                    Year = s.Year.HasValue ? s.Year.Value : DateTime.Now.Year,
                    Id = s.Id
                };
                return PartialView(p);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult StageCreate(PartnershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var p = new Zakar.Models.Partnership
                {
                    Amount = model.Amount,
                    CurrencyId = model.CurrencyId,
                    DateCreated = DateTime.Now,
                    Month = model.Month,
                    PartnerId = model.PartnerId,
                    PartnershipArmId = model.PartnershipArmId,
                    Source = "WEB",
                    Year = model.Year
                };
                _partnershipService.Create(p);
                AccessContext.FlushChanges();
                p.TrackingId = "PO-" + p.Id.ToString(CultureInfo.InvariantCulture);
                _stagedPartnershipService.Delete(model.Id);
                TempData["Message"] = "Partnership Record Logged Successfully";
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult StageDelete(int id, string gridId)
        {
            var m = _stagedPartnershipService.GetSingle(id);
            if (m != null)
            {
                var model = new DeleteConfirmInput()
                {
                    GridId = gridId,
                    Id = id,
                    Message =
                        String.Format("Are you sure you want to delete the staged record for {0} {1}",
                            ((MonthEnums) m.Month).ToString(), m.Year)
                };
                return PartialView("Delete", model);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult StageDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _stagedPartnershipService.Delete(model.Id);
                return Json(new {});
            }
            return PartialView("Delete", model);
        }
        #endregion
    }
}