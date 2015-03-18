using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class LoginModel
    {
        [DataType(DataType.Password), Display(Name = "Password"), Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required, Display(Name = "User name")]
        public string UserName { get; set; }
    }
}