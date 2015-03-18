using PagedList;

namespace Zakar.ViewModels
{
    public class PartnershipSearchModel
    {
        public int CurrencyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Month { get; set; }

        public int PartnershipArmId { get; set; }

        public string PhoneNumber { get; set; }

        public IPagedList<PartnershipResultModel> SearchResults { get; set; }

        public int Year { get; set; }
    }
}