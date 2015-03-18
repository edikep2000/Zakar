namespace Zakar.ViewModels
{
    public class ValidatedPartnershipRecordViewModel
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public MonthEnums Month { get; set; }

        public string PartnershipArm { get; set; }

        public int Year { get; set; }
    }
}