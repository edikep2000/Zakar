using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.Common.FileHandlers;
using Zakar.Controllers.Extensions;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;
using Microsoft.AspNet.Identity;

namespace Zakar.Controllers
{
    [Authorize]
    public class PCFSetupController : BaseController
    {
        private readonly PCFService _pcfService;
        private readonly StagedPCFService _stagedPCFService;
        private readonly ChurchService _churchService;
        private readonly PartnerService _partnerService;
        private readonly PCFExcelFileHandler _pcfExcelFileHandler;

        public PCFSetupController(IUnitOfWork unitOfWork, StagedPCFService stagedPcfService, PCFService pcfService, ChurchService churchService, PartnerService partnerService, PCFExcelFileHandler pcfExcelFileHandler)
            : base(unitOfWork)
        {
            _stagedPCFService = stagedPcfService;
            _pcfService = pcfService;
            _churchService = churchService;
            _partnerService = partnerService;
            _pcfExcelFileHandler = pcfExcelFileHandler;
        }

        #region PCF_CRUD
        public ActionResult Index()
        {
            var church = GetChurchAdmin();
            ViewBag.ModelExists = church != null && _pcfService.GetForChurch(church.Id).Any();
            return View();
        }

        [OutputCache(Duration = 60)]
        public PartialViewResult Create()
        {
            var church = GetChurchAdmin();
            if (church != null)
            {
                var m = new PCFViewModel()
                    {
                        ChurchId = church.Id
                    };
                return PartialView(m);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(PCFViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = AutoMapper.Mapper.Map<PCF>(model);
                m.UniqueId = Zakar.Common.IDGenerators.UniqueIdGenerator.GenerateUniqueIdForPCF(model.Name);
                var church = GetChurchAdmin();
                if (church != null)
                    m.ChurchId = church.Id;
                _pcfService.Insert(m);
                AccessContext.FlushChanges();
                m.UniqueId = "P" + m.Id;
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult Edit(int id)
        {
            var m = _pcfService.GetSingle(id);
            var model = Mapper.Map<PCFViewModel>(m);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(PCFViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = _pcfService.GetSingle(model.Id);
                if (m != null)
                {
                    m.Name = model.Name;
                    m.ChurchId = model.ChurchId;
                    return Json(new {});
                }
                ModelState.AddModelError("", "Cannot Edit A PCF That Does not exist");
            }
            return PartialView();
        }

        public PartialViewResult Delete(int id, string gridId)
        {
            var m = _pcfService.GetSingle(id);
            if (m != null)
            {
                var c = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = m.Id,
                        Message = string.Format("Are you sure you want to delete PCF {0}", m.Name)
                    };
                return PartialView("Delete", c);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                var m = _pcfService.GetSingle(Convert.ToInt32(model.Id));
                if (m != null)
                {
                    var partners = _partnerService.Find(i => i.PCFId == Convert.ToInt32(model.Id));
                    if(partners.Any())
                        partners.UpdateAll(c => c.Set(k => k.PCFId, null));
                    _pcfService.Delete(m);
                }
                return Json(new {});
            }
            return PartialView("Delete", model);
        }

        public ActionResult PCFRead(GridParams g, String search)
        {
            search = (search ?? "").Trim().ToLower();
            var model = _pcfService.Find(I => I.Name.Contains(search)).Select(i => new PCFViewModel()
                {
                    ChurchId = i.ChurchId,
                    Id = i.Id,
                    Name = i.Name,
                    UniqueId = i.UniqueId,
                    ChurchName = i.Church.Name
                });
            return Json(new GridModelBuilder<PCFViewModel>(model, g)
                {
                    Key = "Id",
                    Map = o => new
                        {
                            o.Id,
                            o.ChurchName,
                            o.ChurchId,
                            o.Name,
                            o.UniqueId
                        }
                }.Build());
        }
        #endregion

        #region BulkUpload
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
                    var church = GetChurchAdmin();
                    if (church != null)
                    {
                        _pcfExcelFileHandler.HandleFile(file.FileName, stream, church.Id);
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
            var church = GetChurchAdmin();
            if (church != null)
            {
                ViewBag.ModelExists = _stagedPCFService.Find(i => i.ChurchId == church.Id).Any();
            }
            return View();
        }

        public ActionResult StageRead(GridParams g, string search)
        {
            var church = this.CurrentChurchAdministered().Result;
            search = (search ?? "").Trim().ToLower();
            var model = _stagedPCFService.GetAll().Where(i => i.Name.Contains(search ) && i.ChurchId == church.Id);
            return Json(new GridModelBuilder<StagedPCFs>(model.OrderByDescending(i => i.Id).AsQueryable(), g)
                {
                    Key = "Id",
                    GetItem = () => _stagedPCFService.GetSingle(Convert.ToInt32(g.Key))
                }.Build());
        }


        public PartialViewResult StagePost(int id)
        {
            var m = _stagedPCFService.GetSingle(id);
            if (m != null)
            {
                var model = new PCFViewModel()
                    {
                        ChurchId = m.ChurchId,
                        Name = m.Name,
                        UniqueId = m.UniqueId,
                    };
                return PartialView(model);
            }
            return PartialView();
        }


        [HttpPost]
        public ActionResult StagePost(PCFViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<PCF>(model);
                _pcfService.Insert(m);
                AccessContext.FlushChanges();
                m.UniqueId = "P" + m.Id.ToString();
                _stagedPCFService.Delete(model.Id);
                return Json(new {});
            }
            return PartialView(model);
        }

        public PartialViewResult StageDelete(int id, string gridId)
        {
            var m = _stagedPCFService.GetSingle(id);
            if (m != null)
            {
                var model = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message = String.Format("Are you sure you want to delete staged records for {0}", m.Name)
                    };
                return PartialView("Delete",model);
            }
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult StageDelete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _stagedPCFService.Delete(Convert.ToInt32(model.Id));
                return Json(new {});
            }
            return PartialView("Delete",model);
        }
        #endregion

        #region HelperMethods
        private Church GetChurchAdmin()
        {
            var admin = base.UserStore.FindById(User.Identity.GetUserId<Int32>());
            if (admin.ChurchId.HasValue)
            {
                var church = _churchService.GetChurchForAdmin(admin.ChurchId.Value);
                return church;
            }
            return null;
        }
        #endregion
    }
}