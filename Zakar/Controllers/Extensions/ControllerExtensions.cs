﻿using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}