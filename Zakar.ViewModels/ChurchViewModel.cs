using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class ChurchViewModel
    {
        [Display(Name = "Default Currency")]
        public int? DefaultCurrencyId { get; set; }

        [Display(Name = "Zone")]
        public int GroupId { get; set; }

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}