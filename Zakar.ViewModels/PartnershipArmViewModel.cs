using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class PartnershipArmViewModel
    {
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int Id { get; set; }

        [MinLength(5), Required, MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Abbreviated Form"), Required, StringLength(50)]
        public string ShortFormName { get; set; }
    }
}