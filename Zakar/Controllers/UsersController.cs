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
        private readonly ApplicationUserManager _userManager;
        private readonly IRoleStore<IdentityRole, Int32> _roleStore;
        private readonly GroupService _groupService;
        private readonly ZoneService _zoneService;
        private readonly ChurchService _churchService;
        private readonly UserService _userService;

        public UsersController(IUnitOfWork unitOfWork, IUserStore<IdentityUser, Int32> userstore, IRoleStore<IdentityRole, Int32> roleStore, GroupService groupService, ZoneService zoneService, ChurchService churchService, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(unitOfWork)
        {
            _roleStore = roleStore;
            _groupService = groupService;
            _zoneService = zoneService;
            _churchService = churchService;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = (UserService) userstore;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        #region GroupAdmin
        public async Task<ActionResult> GroupAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.GROUP_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
        public ActionResult GroupAdminRead(GridParams g, string username, string name, string phoneNumber, string groupName)
        {
            var admins =
                 _userManager.Users.Where(
                     i => i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.GROUP_ADMIN.ToString()));
            if (!string.IsNullOrEmpty(username))
                admins = admins.Where(i => i.UserName.Contains(username));
            if (!String.IsNullOrEmpty(name))
                admins = admins.Where(i => i.FirstName.Contains(name) || i.LastName.Contains(name));
            if (!String.IsNullOrEmpty(phoneNumber))
                admins = admins.Where(i => i.PhoneNumber == phoneNumber);
            if (!String.IsNullOrEmpty(groupName))
            {
                var queryable = _groupService.GetAll().Where(i => i.Name.Contains(groupName)).Select(I => I.AdminId);
                admins = admins.Where(i => queryable.Any(k => k == i.Id));
            }

            var model = admins.Select(i => new UserViewModel()
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                PhoneNumber = i.PhoneNumber,
                UserName = i.UserName,
                Id = i.Id,
            });
            return Json(new GridModelBuilder<UserViewModel>(model, g)
            {
                Key = "UserName",
                Map = o => new
                {
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.PhoneNumber,
                    o.UserName,
                }
            }.Build());
        }
        #endregion

        #region PortalAdmins
        public async Task<ActionResult> PortalAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.PORTAL_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != 0);
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

      

        public ActionResult PortalAdminRead(GridParams g, string username, string name, string phoneNumber)
        {
            var admins =
                _userManager.Users.Where(
                    i => i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.PORTAL_ADMIN.ToString()));
            if (!string.IsNullOrEmpty(username))
                admins = admins.Where(i => i.UserName.Contains(username));
            if (!String.IsNullOrEmpty(name))
                admins = admins.Where(i => i.FirstName.Contains(name) || i.LastName.Contains(name));
            if (!String.IsNullOrEmpty(phoneNumber))
                admins = admins.Where(i => i.PhoneNumber == phoneNumber);
            var model = admins.Select(i => new UserViewModel()
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                PhoneNumber = i.PhoneNumber,
                UserName = i.UserName,
                Id = i.Id,
            });
            return Json(new GridModelBuilder<UserViewModel>(model, g)
            {
                Key = "UserName",
                Map = o => new
                {
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.PhoneNumber,
                    o.UserName,
                }
            }.Build());
        }

       
        
        #endregion

        #region ZoneAdmins
        public async Task<ActionResult> ZoneAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.ZONE_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
        public ActionResult ZoneAdminRead(GridParams g, string username, string name, string phoneNumber, string zoneName)
        {

            var admins =
                _userManager.Users.Where(
                    i => i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.ZONE_ADMIN.ToString()));
            if (!string.IsNullOrEmpty(username))
                admins = admins.Where(i => i.UserName.Contains(username));
            if (!String.IsNullOrEmpty(name))
                admins = admins.Where(i => i.FirstName.Contains(name) || i.LastName.Contains(name));
            if (!String.IsNullOrEmpty(phoneNumber))
                admins = admins.Where(i => i.PhoneNumber == phoneNumber);
            if (!String.IsNullOrEmpty(zoneName))
            {
                var queryable = _zoneService.GetAll().Where(i => i.Name.Contains(zoneName)).Select(I => I.AdminId);
                admins = admins.Where(i => queryable.Any(k => k == i.Id));
            }

            var model = admins.Select(i => new UserViewModel()
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                PhoneNumber = i.PhoneNumber,
                UserName = i.UserName,
                Id = i.Id,
            });
            return Json(new GridModelBuilder<UserViewModel>(model, g)
            {
                Key = "UserName",
                Map = o => new
                {
                    o.Id,
                    o.FirstName,
                    o.LastName,
                    o.PhoneNumber,
                    o.UserName,
                }
            }.Build());
        }

     
        #endregion

        #region ChapterAdmins
        public async Task<ActionResult> ChapterAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.CHAPTER_ADMIN.ToString());
            var userExistsInRole = t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
        public ActionResult ChapterAdminRead(GridParams g, string username, string name, string phoneNumber, string chapterName)
        {
            var chapterAdmins = 
                _userManager.Users.Where(
                    i => i.IdentityUserInRoles.Any(k => k.IdentityRole.Name == RolesEnum.CHAPTER_ADMIN.ToString()));
            if (!string.IsNullOrEmpty(username))
                chapterAdmins = chapterAdmins.Where(i => i.UserName.Contains(username));
            if (!String.IsNullOrEmpty(name))
                chapterAdmins = chapterAdmins.Where(i => i.FirstName.Contains(name) || i.LastName.Contains(name));
            if (!String.IsNullOrEmpty(phoneNumber))
                chapterAdmins = chapterAdmins.Where(i => i.PhoneNumber == phoneNumber);
            if (!String.IsNullOrEmpty(chapterName))
            {
                var chapter = _churchService.GetAll().Where(i => i.Name.Contains(chapterName)).Select(I => I.AdminId);
                chapterAdmins = chapterAdmins.Where(i => chapter.Any(k => k == i.Id));
            }

            var model = chapterAdmins.Select(i => new UserViewModel()
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    PhoneNumber = i.PhoneNumber,
                    UserName = i.UserName,
                    Id = i.Id,
                });
            return Json(new GridModelBuilder<UserViewModel>(model, g)
                {
                    Key = "UserName",
                    Map = o => new
                        {
                            o.Id,
                            o.FirstName,
                            o.LastName,
                            o.PhoneNumber,
                             UserName = o.UserName.Trim(),
                        }
                }.Build());
        }

        public async Task<PartialViewResult> Delete(int id, string gridId)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new DeleteConfirmInput()
                    {
                        GridId = gridId,
                        Id = id,
                        Message = String.Format("Are you sure you want to delete {0} {1} with username {2}", user.LastName, user.FirstName, user.UserName)
                    };
                return PartialView("model");
            }
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(DeleteConfirmInput model)
        {
            if (ModelState.IsValid)
            {
                var m = await _userManager.FindByIdAsync(model.Id);
                if(m != null)
                {
                var t =  await  _userManager.DeleteAsync(m);
                    if (t.Succeeded)
                    {
                        return Json(new {});
                    }
                }
            }
            ModelState.AddModelError("","Unable to delete user");
            return PartialView(model);
        } 
        #endregion
	}
}