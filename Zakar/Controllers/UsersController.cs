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
    [Authorize]
    public class UsersController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IRoleStore<IdentityRole, Int32> _roleStore;
        private readonly GroupService _groupService;
        private readonly ZoneService _zoneService;
        private readonly ChurchService _churchService;

        public UsersController(IUnitOfWork context, IRoleStore<IdentityRole, Int32> roleStore, GroupService groupService, ZoneService zoneService, ChurchService churchService, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : base(context)
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
        
     

        #region PortalAdmins
        public async Task<ActionResult> PortalAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.PORTAL_ADMIN.ToString());
            var userExistsInRole = t != null && t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
                    UserName = model.Username,
                    FailedAccessAttempts = 0,
                };
                var role = await _roleStore.FindByNameAsync(RolesEnum.PORTAL_ADMIN.ToString());
                IdentityResult result = IdentityResult.Failed();
                if (role != null)
                {
                    user.IdentityUserInRoles.Add(new IdentityUserInRole()
                        {
                            IdentityRole = role,
                            IdentityUser = user
                        });
                    result = await _userManager.CreateAsync(user, model.Password);
                }

                if (result.Succeeded)
                {
                 
                       return Json(new { });
                    
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
            var userExistsInRole = t != null && t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
                    UserName = model.Username,
                    FailedAccessAttempts = 0,
                };
                var role = await _roleStore.FindByNameAsync(RolesEnum.ZONE_ADMIN.ToString());
                IdentityResult result = IdentityResult.Failed();
                 var zone = _zoneService.GetSingle(model.ZoneId);
                if (role != null && zone != null)
                {
                    user.IdentityUserInRoles.Add(new IdentityUserInRole()
                    {
                        IdentityRole = role,
                        IdentityUser = user
                    });
                    user.ZoneId = zone.Id;
                    result = await _userManager.CreateAsync(user, model.Password);
                }

                if (result.Succeeded)
                {
                   
                        return Json(new { });
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
                var queryable = _zoneService.GetAll().Where(i => i.Name.Contains(zoneName)).Select(I => I.Id);
                admins = admins.Where(i => queryable.Any(k => k == i.ZoneId));
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

        #region GroupAdmin
        public async Task<ActionResult> GroupAdmins()
        {
            var t = await _roleStore.FindByNameAsync(RolesEnum.GROUP_ADMIN.ToString());
            var userExistsInRole = t != null && t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
                    UserName = model.Username,
                    FailedAccessAttempts = 0,
                };
                var role = await _roleStore.FindByNameAsync(RolesEnum.GROUP_ADMIN.ToString());
                var group = _groupService.GetSingle(model.GroupId);
                var result = IdentityResult.Failed();
                if (group != null && role != null)
                {
                    user.IdentityUserInRoles.Add(new IdentityUserInRole()
                        {
                            IdentityRole = role,
                            IdentityUser = user
                        });
                    user.GroupId = group.Id;
                    result = await _userManager.CreateAsync(user, model.Password);
                }
               
                if (result.Succeeded)
                {
                   return Json(new { });
                }
                if (result.Errors != null && result.Errors.Any())
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
                var queryable = _groupService.GetAll().Where(i => i.Name.Contains(groupName)).Select(I => I.Id);
                admins = admins.Where(i => queryable.Any(k => k == i.GroupId));
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
            var userExistsInRole = t != null && t.IdentityUserInRoles.Any(i => i.UserId != 0);
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
                    UserName = model.Username,
                    FailedAccessAttempts = 0,
                    
                   
                };
                var result = IdentityResult.Failed();
                var role = await _roleStore.FindByNameAsync(RolesEnum.CHAPTER_ADMIN.ToString());
                var chapter = _churchService.GetSingle(model.ChapterId);
                if (role != null && chapter != null)
                {
                    user.IdentityUserInRoles.Add(new IdentityUserInRole()
                        {
                            IdentityRole = role,
                            IdentityUser = user
                        });
                    user.ChurchId = model.ChapterId;
                    result = await _userManager.CreateAsync(user, model.Password);

                }
              
                if (result.Succeeded)
                {

                    return Json(new { });
                }
                if(result.Errors != null && result.Errors.Any())
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
                var chapter = _churchService.GetAll().Where(i => i.Name.Contains(chapterName)).Select(I => I.Id);
                chapterAdmins = chapterAdmins.Where(i => chapter.Any(k => k == i.ChurchId));
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
                return PartialView("Delete", model);
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
            return PartialView("Delete", model);
        } 
        #endregion
	}
}