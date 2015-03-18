using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class LocalPasswordModel
    {
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match."), Display(Name = "Confirm new password"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6), DataType(DataType.Password), Display(Name = "New password"), Required]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Required, Display(Name = "Current password")]
        public string OldPassword { get; set; }
    }
}