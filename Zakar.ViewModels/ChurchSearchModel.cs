using PagedList;

namespace Zakar.ViewModels
{
    public class ChurchSearchModel
    {
        public string Name { get; set; }

        public IPagedList<ChurchViewModel> SearchResults { get; set; }
    }
}