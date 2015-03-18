namespace Zakar.ViewModels
{
    public class NonValidatedPartnershipRecordViewModel
    {
        public decimal Amount { get; set; }

        public int Currency { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

        public MonthEnums Month { get; set; }

        public int PartnerId { get; set; }

        public string PartnershipArm { get; set; }

        public int PartnershipArmId { get; set; }

        public int Year { get; set; }
    }
}