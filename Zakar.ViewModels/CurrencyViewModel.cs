using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class CurrencyViewModel
    {
        [Required, Range(0, 0xf4240)]
        [Display(Name = "Conversion Rate To Default")]
        public decimal ConversionRateToDefault { get; set; }

        public int Id { get; set; }

        [Display(Name = "Is Default Currency")]
        public bool IsDefaultCurrency { get; set; }

        [Display(Name = "Name")]
        [StringLength(50), Required]
        public string Name { get; set; }

        [Display(Name = "Symbol")]
        [Required, StringLength(3)]
        public string Symbol { get; set; }
    }
}