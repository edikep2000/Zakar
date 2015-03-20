using System;
using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class PartnerFactory
    {
        public static Partner BuildNew()
        {
            return new Partner { Deleted = false, DateCreated = DateTime.Now, DateDeleted = new DateTime?(DateTime.Now.AddYears(-1)) };
        }
    }
}