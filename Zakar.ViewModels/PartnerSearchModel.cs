using PagedList;

namespace Zakar.ViewModels
{
    public class PartnerSearchModel
    {
        public string EmailSearchString { get; set; }

        public string FirstNameSearchString { get; set; }

        public string LastNameSearchString { get; set; }

        public IPagedList<PartnerViewModel> PartnerSearchResults { get; set; }

        public string PhoneNumberSearchString { get; set; }

        public string YookosIdSearchString { get; set; }
    }
}