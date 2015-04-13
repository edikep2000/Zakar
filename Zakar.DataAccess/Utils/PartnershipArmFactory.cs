using System;
using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class PartnershipArmFactory
    {
        public static PartnershipArm BuildNew()
        {
            return new PartnershipArm();
        }
    }
}