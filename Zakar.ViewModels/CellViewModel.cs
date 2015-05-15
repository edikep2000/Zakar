using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class CellViewModel
    {
        [Required]
        [Display(Name = "PCF")]
        public int PCFId { get; set; }



        public string UniqueId { get; set; }
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public string PCFName { get; set; }
    }
}