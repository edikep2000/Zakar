using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class SMSMessageViewModel
    {
        [Display(Name = "SMS Message"), Required, StringLength(160)]
        public string Message { get; set; }

        [Display(Name = "Phone Number"), StringLength(15), Required]
        public string PhoneNumber { get; set; }
    }
}