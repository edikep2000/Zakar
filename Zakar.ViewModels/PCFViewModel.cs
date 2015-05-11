using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class PCFViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "PCF Name")]
        public string Name { get; set; }

        [Display(Name = "Unique Id")]
        public string UniqueId { get; set; }

        [Display(Name = "Church")]
        [Required]
        public int ChurchId { get; set; }
    }
}