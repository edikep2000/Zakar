using System;

namespace Zakar.ViewModels
{
    public class PartnershipResultModel
    {
        public decimal Amount { get; set; }

        public string CurrencySymbol { get; set; }

        public int CurrencySymbolId { get; set; }

        public DateTime DateLogged { get; set; }

        public int Month { get; set; }

        public int PartnerId { get; set; }

        public string PartnersFullName { get; set; }

        public string PartnershipArm { get; set; }

        public int PartnershipArmId { get; set; }

        public long PartnershipId { get; set; }

        public int Year { get; set; }
    }
}