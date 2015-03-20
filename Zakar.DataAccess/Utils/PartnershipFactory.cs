using System;
using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class PartnershipFactory
    {
        public static Partnership BuildNew()
        {
            return new Partnership { Amount = 0M, Year = 0x7dd, Month = 1, DateCreated = new DateTime?(DateTime.Now.Date) };
        }
    }
}