using System;
using Microsoft.AspNet.Identity;

namespace Zakar.Models
{
    public partial class IdentityRole : IRole
    {
        public IdentityRole()
        {
            _id = Guid.NewGuid().ToString();
        }

        public IdentityRole(String name)
        {
            _name = name;
        }

        public IdentityRole(string name, string id)
        {
            _name = name;
            _id = id;
        }
    }
}