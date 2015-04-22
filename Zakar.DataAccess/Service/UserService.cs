using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class UserService : IUserStore<IdentityUser>, IUserClaimStore<IdentityUser>, IUserRoleStore<IdentityUser>, IUserLoginStore<IdentityUser>, IUserPasswordStore<IdentityUser>, IUserEmailStore<IdentityUser>
    {
        private readonly IRepository<IdentityUser> _userRepo;
        private readonly IRepository<IdentityRole> _roleRepo;
        private readonly IRepository<IdentityUserClaim> _claimsRepo;
        private readonly IRepository<IdentityUserInRole> _userInRoleRepo;
        private readonly IRepository<IdentityUserLogin> _userLoginRepo; 
 
        public UserService(IRepository<IdentityUser> userRepo, IRepository<IdentityRole> roleRepo, IRepository<IdentityUserClaim> claimsRepo, IRepository<IdentityUserInRole> userInRoleRepo, IRepository<IdentityUserLogin> userLoginRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _claimsRepo = claimsRepo;
            _userInRoleRepo = userInRoleRepo;
            _userLoginRepo = userLoginRepo;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(IdentityUser user)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            _userRepo.Insert(user);
            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(IdentityUser user)
        {
           if(user == null)
               throw new ArgumentNullException("user");
            //TODO Update the data store
            return Task.FromResult<Object>(null);
        }

        public Task DeleteAsync(IdentityUser user)
        {
           if(user == null)
               throw new ArgumentNullException("user");
            _userRepo.Delete(user);
            return Task.FromResult<object>(null);
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
           if(String.IsNullOrEmpty(userId))
               throw new ArgumentNullException("userId");
            var result = _userRepo.Find(i => i.Id == userId).FirstOrDefault();
            return result != null ? Task.FromResult(result) : Task.FromResult<IdentityUser>(null);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            if(string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            var result = _userRepo.Find(i => i.UserName.Equals(userName.ToLower())).FirstOrDefault();
            return result != null ? Task.FromResult(result) : Task.FromResult<IdentityUser>(null);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = _claimsRepo.Find(i => i.UserId == user.Id).Select(b => new Claim(b.ClaimType, b.ClaimValue)).ToList();
            return Task.FromResult<IList<Claim>>(claims);
        }

        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
           if(user == null)
               throw new ArgumentNullException("user");
            if(claim == null)
                throw new ArgumentNullException("claim");
            _claimsRepo.Insert(new IdentityUserClaim()
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    UserId = user.Id
                });
            return Task.FromResult<object>(null);
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            if(claim == null)
                throw new ArgumentNullException("claim");
            var m =
                _claimsRepo.Find(i => i.UserId == user.Id && i.ClaimType == claim.Type && i.ClaimValue == claim.Value)
                           .FirstOrDefault();
            _claimsRepo.Delete(m);
            return Task.FromResult<Object>(null);
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            var role = _roleRepo.Find(i => i.Name.Equals(roleName)).FirstOrDefault();
            if(role == null)
                throw new Exception(String.Format("Role {0} does not exist", roleName));
            _userInRoleRepo.Insert(new IdentityUserInRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            return Task.FromResult<Object>(null);
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            if(String.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            var role = _roleRepo.Find(i => i.Name.Equals(roleName)).FirstOrDefault();
            if(role == null)
                throw new Exception(String.Format("Role {0} does not exist", roleName));
            var m = _userInRoleRepo.Find(i => i.UserId == user.Id && i.RoleId == role.Id).FirstOrDefault();
            _userInRoleRepo.Delete(m);
            return Task.FromResult<Object>(null);


        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var roleNames = _roleRepo.Find(i => i.IdentityUserInRoles.Any(j => j.UserId == user.Id)).Select(i => i.Name);
            return Task.FromResult<IList<String>>(roleNames.ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
           if(user == null)
               throw new ArgumentNullException("user");
            if(string.IsNullOrEmpty(roleName))
                throw new ArgumentNullException("roleName");
            var role =
                _userInRoleRepo.Find(i => i.UserId == user.Id && i.IdentityRole.Name.Equals(roleName)).FirstOrDefault();
            return Task.FromResult(role != null);
        }

        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if(user == null)
                throw new ArgumentNullException("user");
            if(login == null)
                throw new ArgumentNullException("login");
            _userLoginRepo.Insert(new IdentityUserLogin()
                {
                    Id = user.Id,
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.ProviderKey
                });
            return Task.FromResult<Object>(null);
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if(login == null)
                throw new ArgumentNullException("login");
            if(user == null)
                throw new ArgumentNullException("user");
            var m =
                _userLoginRepo.Find(
                    i => i.Id == user.Id && i.ProviderKey == login.ProviderKey && i.LoginProvider == login.LoginProvider)
                              .FirstOrDefault();
            if(m != null)
                _userLoginRepo.Delete(m);
            return Task.FromResult<Object>(null);

        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
           if(user == null)
               throw new ArgumentNullException("user");
            var m =
                _userLoginRepo.Find(i => i.Id == user.Id).Select(t => new UserLoginInfo(t.LoginProvider, t.ProviderKey));
            return Task.FromResult<IList<UserLoginInfo>>(m.ToList());
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
           if(login == null)
               throw new ArgumentNullException("login");
            var m =
                _userLoginRepo.Find(i => i.ProviderKey == login.ProviderKey && i.LoginProvider == login.LoginProvider)
                              .FirstOrDefault();
            if (m == null)
                return Task.FromResult<IdentityUser>(null);
            return Task.FromResult<IdentityUser>(m.IdentityUser);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult<Object>(null);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
           if (user == null)
           {
               throw new ArgumentNullException("user");
           }

            var u = _userRepo.Find(i => i.Id == user.Id).FirstOrDefault();
            return u == null ? Task.FromResult<String>(null) : Task.FromResult(u.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");

            }
            var m = _userRepo.Find(i => i.Id == user.Id).FirstOrDefault();
            var hasPassword = (m != null) && !String.IsNullOrEmpty(m.PasswordHash);
            return Task.FromResult(hasPassword);
        }

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            var t = _userRepo.Find(i => i.UserName == email).FirstOrDefault();
            return Task.FromResult(t);
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            if (user == null || String.IsNullOrEmpty(user.UserName))
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }



        public Task SetEmailAsync(IdentityUser user, string email)
        {
            user.UserName = email;
            return Task.FromResult<object>(0);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            return Task.FromResult<object>(null);
        }
    }
}