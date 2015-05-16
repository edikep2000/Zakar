using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.Common.Enums;
using Zakar.Common.FileHandlers;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    [Authorize]
    public class PartnerController : BaseController
    {
        private readonly PartnerService _partnerService;
        private readonly StagedPartnerService _stagedPartnerService;
        private readonly ChurchService _churchService;
        private readonly PartnerExcelFileHandler _fileHandler;
        private readonly CellService _cellService;
        private readonly PCFService _pcfService;
        public PartnerController(IUnitOfWork unitOfWork, ChurchService churchService, StagedPartnerService stagedPartnerService, PartnerService partnerService, PartnerExcelFileHandler fileHandler, CellService cellService, PCFService pcfService)
            : base(unitOfWork)
        {
            _churchService = churchService;
            _stagedPartnerService = stagedPartnerService;
            _partnerService = partnerService;
            _fileHandler = fileHandler;
            _cellService = cellService;
            _pcfService = pcfService;
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
                    m.UniqueId = Zakar.Common.IDGenerators.UniqueIdGenerator.GenerateUniqueIdForPartner(String.Format("{0} {1}", model.LastName, model.FirstName));
                    _partnerService.Insert(m);
                    base.AccessContext.FlushChanges();
                    m.UniqueId = "P" + m.Id.ToString();
                    //TODO, Send SMS And Or Email To the Partner with New Id
                    return Json(new { });
                }
                ModelState.AddModelError("", "Partner Must Belong to A Church");
            }
            return PartialView(model);
        }


        public ActionResult PartnerRead(GridParams g, string search, int cellId = 0, int pcfId = 0, int churchId = 0, int groupId = 0, int zoneId = 0)
        {
            search = (search ?? "").Trim();
            var query = _partnerService.GetAll().Where(i => i.FirstName.Contains(search) || i.LastName.Contains(search));

            if (churchId != 0)
            {
                query = query.Where(i => i.ChurchId == churchId);
            }
            else if (User.IsInRole(RolesEnum.CHAPTER_ADMIN.ToString()))
            {
                query = query.Where(i => i.ChurchId == GetChurchAdmin().Id);
            }
            if (pcfId != 0)
                query = query.Where(i => i.PCFId == pcfId);
            if (cellId != 0)
                query = query.Where(i => i.CellId == cellId);
            var cells = GetCells();
            var pcfs = GetPCFs();

            var model = query.Select(i => new PartnerListModel
            {
                Id = i.Id,
                CellId = i.CellId,
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
                ZoneName = i.Church.Group.Zone.Name,
                CellName = i.CellId.HasValue ? cells.FirstOrDefault(m => m.Key == i.CellId.Value).Value : "No Cell",
                PCFName = i.PCFId.HasValue ? pcfs.FirstOrDefault(m => m.Key == i.PCFId.Value).Value : "No PCF"
            });
            return Json(new GridModelBuilder<PartnerListModel>(model.OrderByDescending(I => I.DateCreated), g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.CellId,
                    o.CellName,
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
                    o.PCFName,
                    o.PCFId
                }
            }.Build());
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
                return PartialView("Delete",model);
            }
            return PartialView("Delete");
        }
        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                _partnerService.Delete(Convert.ToInt32(model.Id));
                return Json(new { });
            }
            return PartialView("Delete",model);
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
                catch (Exception e)
                {

                    ModelState.AddModelError("", e.Message);

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

        public ActionResult StagePartnerRead(GridParams g, string search, int cellId = 0, int pcfId = 0)
        {
            search = (search ?? "").Trim();
            var query = _stagedPartnerService.GetAll().Where(i => i.ChurchId == GetChurchAdmin().Id && (i.FirstName.Contains(search) || i.LastName.Contains(search)));
            if (pcfId != 0)
                query = query.Where(i => i.PCFId == pcfId);
            if (cellId != 0)
                query = query.Where(i => i.CellId == cellId);
            var cells = GetCells();
            var pcfs = GetPCFs();
            var model = query.Select(i => new PartnerListModel
            {
                Id = i.Id,
                CellId = i.CellId,
                ChurchId = i.ChurchId.HasValue ? i.ChurchId.Value : 0,
                DateCreated = i.DateCreated.HasValue ? i.DateCreated.Value : DateTime.Now,
                DateOfBirth = i.DateOfBirth,
                Email = i.Email,
                FullName = i.LastName + " " + i.FirstName,
                Gender = i.Gender,
                Phone = i.Phone,
                Title = i.Title,
                UniqueId = i.UniqueId,
                CellName = i.CellId.HasValue ? cells.FirstOrDefault(m => m.Key == i.CellId.Value).Value : "No Cell",
                PCFName = i.PCFId.HasValue ? pcfs.FirstOrDefault(m => m.Key == i.PCFId.Value).Value : "No PCF"
            });
            return Json(new GridModelBuilder<PartnerListModel>(model.OrderByDescending(I => I.DateCreated), g)
            {
                Key = "Id",
                Map = o => new
                {
                    o.Id,
                    o.CellId,
                    o.CellName,
                    o.ChurchId,
                    o.DateCreated,
                    o.DateOfBirth,
                    o.Email,
                    o.FullName,
                    o.Gender,
                    o.Phone,
                    o.Title,
                    o.UniqueId,
                    o.PCFId,
                    o.PCFName
                }
            }.Build());
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
                        UniqueId = s.UniqueId,
                        DateOfBirth = s.DateOfBirth.HasValue ? s.DateOfBirth.Value : DateTime.Now,
                        Gender = s.Gender
                    };
                return PartialView(m);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult StageCreate(PartnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var m = Mapper.Map<Partner>(model);
                _partnerService.Insert(m);
                base.AccessContext.FlushChanges();
                m.UniqueId = "P" + m.Id;
                _stagedPartnerService.Delete(model.Id);
                //TODO: Send Email and Or SMS with the Partners Id
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
        private  Church GetChurchAdmin()
        {
            var user = base.UserStore.FindById(User.Identity.GetUserId<Int32>());
            if (user.ChurchId.HasValue)
            {
                var church = _churchService.GetChurchForAdmin(user.ChurchId.Value);
                return church;
            }
            return null;
        }

        private Dictionary<int, string> GetCells()
        {
            var cell = _cellService.GetCellInChurch(GetChurchAdmin().Id);
            return cell.ToDictionary(c => c.Id, v => v.Name);
        } 
        private Dictionary<int, String> GetPCFs()
        {
            var church = _pcfService.GetForChurch(GetChurchAdmin().Id);
            return church.ToDictionary(c => c.Id, v => v.Name);
        }
        #endregion
    }
}