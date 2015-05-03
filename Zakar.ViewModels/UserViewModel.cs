using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class UserViewModel
    {
        [Required, Display(Name = "Church Administered")]
        public int EntityId { get; set; }

        [Display(Name = "First Name"), Required, DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name"), DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number"), DataType(DataType.PhoneNumber), Required]
        public string PhoneNumber { get; set; }

        public string Id { get; set; }

        [Display(Name = "Email"), DataType(DataType.EmailAddress), Required]
        public string UserName { get; set; }
    }
}