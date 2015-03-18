using Zakar.Models;

namespace Zakar.ViewModels
{
    public class PartnerPartnershipObject
    {
        public decimal Amount { get; set; }

        public string AmountString { get; set; }

        public string CurrencySymbol { get; set; }

        public string DefaultCurrencyString { get; set; }

        public decimal DenominatedCurrencyAmount { get; set; }

        public int Id { get; set; }

        public string Month { get; set; }

        public Partner Partner { get; set; }

        public string PartnerName { get; set; }

        public int Year { get; set; }
    }
}