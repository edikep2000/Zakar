using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class EmailMessageViewModel
    {
        [Display(Name = "Body"), Required]
        public string Body { get; set; }

        [Required, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Subject"), Required, StringLength(100)]
        public string Subject { get; set; }
    }
}