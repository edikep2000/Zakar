using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.OpenAccess;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    [Authorize]
    public class PartnershipController : BaseController
    {
        //
        // GET: /Partnership/
        public PartnershipController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

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
            return Json(new { });
        }

        public PartialViewResult Edit(int id = 0)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(PartnershipViewModel model)
        {
            return Json(new { });
        }

        public PartialViewResult Delete(int id, string gridId)
        {
            return PartialView();
        }

        public ActionResult Delete(DeleteConfirmInput model)
        {
            return Json(new { });
        } 
        #endregion

        #region Bulk Methods

        public ActionResult Bulk()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Bulk(HttpPostedFileBase file)
        {
            return View();
        }

        public ActionResult Stage()
         {
          return View();
         }


        public ActionResult StageCreate(int id)
        {
            return PartialView();
        }


        public ActionResult StageCreate(PartnershipViewModel model)
        {
            return Json(new {});
        }

        public PartialViewResult StageDelete(int id, string gridId)
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult StageDelete(DeleteConfirmInput model)
        {
            return Json(new {});
        }
        #endregion
    }
}