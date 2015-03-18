using PagedList;

namespace Zakar.ViewModels
{
    public class UserSearchModel
    {
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public IPagedList<UserViewModel> UserProfileResults { get; set; }
    }
}