using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Select Zone")]
        public int ZoneId { get; set; }


        [Display(Name = "Unique Id")]
        public string UniqueId { get; set; }
    }
}