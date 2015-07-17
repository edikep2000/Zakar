using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Zakar.App_Start;
using Zakar.Common.Enums;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers.Extensions
{
    public static class ControllerExtensions
    {
        public static IdentityUser CurrentUser(this Controller controller)
        {
            var userService = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var user = userService.FindById(controller.User.Identity.GetUserId<Int32>());
            return user;
        }
        public static async Task<Church> CurrentChurchAdministered(this Controller controller)
        {
            var userService = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var churchService = DependencyResolver.Current.GetService<ChurchService>();
            var user = await userService.FindByIdAsync(controller.User.Identity.GetUserId<Int32>());
            if (
                user.IdentityUserInRoles.FirstOrDefault(i => i.IdentityRole.Name == RolesEnum.CHAPTER_ADMIN.ToString()) ==
                null)
                return null;

            else
            {
                var churchId = user.ChurchId.HasValue ? user.ChurchId.Value : 0;
                var c = churchService.GetSingle(churchId);
                return c;
            }

        }

        public static async Task<Group> CurrentGroupAdministered(this Controller controller)
        {
            var userService = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var churchService = DependencyResolver.Current.GetService<GroupService>();
            var user = await userService.FindByIdAsync(controller.User.Identity.GetUserId<Int32>());
            if (
                user.IdentityUserInRoles.FirstOrDefault(i => i.IdentityRole.Name == RolesEnum.GROUP_ADMIN.ToString()) ==
                null)
                return null;

            else
            {
                var groupId = user.GroupId.HasValue ? user.GroupId.Value : 0;
                var c = churchService.GetSingle(groupId);
                return c;
            }
        }

        public static async Task<Zone> CurrentZoneAdministered(this Controller controller)
        {
            var userService = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var churchService = DependencyResolver.Current.GetService<ZoneService>();
            var user = await userService.FindByIdAsync(controller.User.Identity.GetUserId<Int32>());
            if (
                user.IdentityUserInRoles.FirstOrDefault(i => i.IdentityRole.Name == RolesEnum.ZONE_ADMIN.ToString()) ==
                null)
                return null;

            else
            {
                var groupId = user.ZoneId.HasValue ? user.ZoneId.Value : 0;
                var c = churchService.GetSingle(groupId);
                return c;
            }
        }

        public static decimal Round(this decimal d)
        {
            return Decimal.Round(d, 2);
        }

    }
}