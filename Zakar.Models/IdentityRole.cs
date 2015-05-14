using System;
using Microsoft.AspNet.Identity;

namespace Zakar.Models
{
    public partial class IdentityRole : IRole<Int32>
    {
        public IdentityRole()
        {
            
        }

        public IdentityRole(String name)
        {
            _name = name;
        }

        public IdentityRole(string name, int id)
        {
            _name = name;
            _id = id;
        }
    }
}