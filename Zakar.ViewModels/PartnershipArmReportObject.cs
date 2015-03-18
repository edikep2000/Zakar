namespace Zakar.ViewModels
{
    public class PartnershipArmReportObject
    {
        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public decimal DenominatedAmount { get; set; }

        public string DenominatedCurrencyName { get; set; }

        public int Month { get; set; }

        public int PartnerId { get; set; }

        public string PartnerName { get; set; }

        public int PartnershipArmId { get; set; }

        public string PartnershipArmName { get; set; }

        public int Year { get; set; }
    }
}