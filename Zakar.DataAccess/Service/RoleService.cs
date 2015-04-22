using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class RoleService : IRoleStore<IdentityRole>
    {
        private readonly IRepository<IdentityRole> _roleStore;
         

        public RoleService(IRepository<IdentityRole> roleStore)
        {
            _roleStore = roleStore;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(IdentityRole role)
        {
            if(role == null)
                throw new ArgumentNullException("role");
            var find = FindByNameAsync(role.Name).Result;
            if(find != null)
                throw new Exception("Role name already taken");
            _roleStore.Insert(new IdentityRole(role.Name));
            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(IdentityRole role)
        {
            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if(role == null)
                throw new ArgumentNullException("role");
            var r = FindByIdAsync(role.Id).Result;
            _roleStore.Delete(r);
            return Task.FromResult<Object>(null);
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            var item = _roleStore.Find(i => i.Id == roleId).FirstOrDefault();
            return Task.FromResult<IdentityRole>(item);

        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var item = _roleStore.Find(i => i.Name == roleName).FirstOrDefault();
            return Task.FromResult<IdentityRole>(item);
        }

        public Task<IEnumerable<IdentityRole>> FindAll()
        {
            var t = _roleStore.GetAll().AsEnumerable();
            return Task.FromResult(t);
        }
    }
}