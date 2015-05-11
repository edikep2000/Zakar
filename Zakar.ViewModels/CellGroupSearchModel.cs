using PagedList;

namespace Zakar.ViewModels
{
    public class CellGroupSearchModel
    {
        public int ChurchId { get; set; }

        public string Name { get; set; }

        public IPagedList<CellViewModel> SearchResults { get; set; }
    }
}