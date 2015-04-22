using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Omu.AwesomeMvc;
using Telerik.OpenAccess;
using Zakar.App_Start;
using Zakar.Common.Enums;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class UsersController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IRoleStore<IdentityRole> _roleStore;
        private readonly GroupService _groupService;
        private readonly ZoneService _zoneService;
        private readonly ChurchService _churchService;
 
        public UsersController(IUnitOfWork unitOfWork, IRoleStore<IdentityRole> roleStore, GroupService groupService, ZoneService zoneService, ChurchService churchService, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(unitOfWork)
        {
            _roleStore = roleStore;
            _groupService = groupService;
            _zoneService = zoneService;
            _churchService = churchService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        #region GroupAdmin
        public async Task<ActionResult> GroupAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.GROUP_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != "0");
            ViewBag.UserExists = userExistsInRole;
            return View();
        }

        public PartialViewResult GroupAdminCreate()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> GroupAdminCreate(GroupAdminRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    DateCreated = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var s = await _userManager.AddToRoleAsync(user.Id, RolesEnum.GROUP_ADMIN.ToString());
                    if (s.Succeeded)
                    {
                        var group = _groupService.GetSingle(model.GroupId);
                        if (group != null)
                        {
                            group.AdminId = user.Id;
                        }
                        return Json(new { });

                    }
                    else if (s.Errors != null && s.Errors.Any())
                    {
                        foreach (var e in s.Errors)
                        {
                            ModelState.AddModelError("", e);
                        }
                    }
                }
                else if (result.Errors != null && result.Errors.Any())
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult GroupAdminRead(GridParams g)
        {
            return Json(new { });
        }

        public PartialViewResult GroupAdminDelete(string id, string gridId)
        {
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult GroupAdminDelete(DeleteConfirmInput model)
        {
            return Json(new {});
        } 
        #endregion

        #region PortalAdmins
        public async Task<ActionResult> PortalAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.PORTAL_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != "0");
            ViewBag.UserExists = userExistsInRole;
            return View();
        }

        public PartialViewResult PortalAdminCreate()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> PortalAdminCreate(PortalAdminRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    DateCreated = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var s = await _userManager.AddToRoleAsync(user.Id, RolesEnum.PORTAL_ADMIN.ToString());
                    if (s.Succeeded)
                    {
                       return Json(new { });
                    }
                    if (s.Errors != null && s.Errors.Any())
                    {
                        foreach (var e in s.Errors)
                        {
                            ModelState.AddModelError("", e);
                        }
                    }
                }
                else if (result.Errors != null && result.Errors.Any())
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }
            return PartialView(model);
        }

      

        public ActionResult PortalAdminRead(GridParams g)
        {
            return Json(new { });
        }

        public PartialViewResult PortalAdminDelete(string id, string gridId)
        {
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult PortalAdminDelete(DeleteConfirmInput model)
        {
            return Json(new { });
        } 
        
        #endregion

        #region ZoneAdmins
        public async Task<ActionResult> ZoneAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.ZONE_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != "0");
            ViewBag.UserExists = userExistsInRole;
            return View();
        }

        public PartialViewResult ZoneAdminCreate()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> ZoneAdminCreate(ZonalAdminRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    DateCreated = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var s = await _userManager.AddToRoleAsync(user.Id, RolesEnum.ZONE_ADMIN.ToString());
                    if (s.Succeeded)
                    {
                        var zone = _zoneService.GetSingle(model.ZoneId);
                        if (zone != null)
                        {
                            zone.AdminId = user.Id;
                        }
                        return Json(new { });

                    }
                    else if (s.Errors != null && s.Errors.Any())
                    {
                        foreach (var e in s.Errors)
                        {
                            ModelState.AddModelError("", e);
                        }
                    }

                }
                else if (result.Errors != null && result.Errors.Any())
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ZoneAdminRead(GridParams g)
        {
            return Json(new { });
        }

        public PartialViewResult ZoneAdminDelete(string id, string gridId)
        {
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult ZoneAdminDelete(DeleteConfirmInput model)
        {
            return Json(new { });
        } 
        #endregion

        #region ChapterAdmins
        public async Task<ActionResult> ChapterAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.CHAPTER_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != "0");
            ViewBag.UserExists = userExistsInRole;
            return View();
        }

        public PartialViewResult ChapterAdminCreate()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> ChapterAdminCreate(ChapterAdminRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    DateCreated = DateTime.Now,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    UserName = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var s = await _userManager.AddToRoleAsync(user.Id, RolesEnum.CHAPTER_ADMIN.ToString());
                    if (s.Succeeded)
                    {
                        var church = _churchService.GetSingle(model.ChapterId);
                        if (church != null)
                        {
                            church.AdminId = user.Id;
                         }
                        return Json(new {});
                      
                    }
                    else if(s.Errors != null && s.Errors.Any())
                    {
                        foreach (var e in s.Errors)
                        {
                            ModelState.AddModelError("", e);
                        }
                    }
                   
                }
                else if(result.Errors != null && result.Errors.Any())
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ChapterAdminRead(GridParams g)
        {
            return Json(new { });
        }

        public PartialViewResult ChapterAdminDelete(string id, string gridId)
        {
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult ChapterAdminDelete(DeleteConfirmInput model)
        {
            return Json(new { });
        } 
        #endregion
	}
}