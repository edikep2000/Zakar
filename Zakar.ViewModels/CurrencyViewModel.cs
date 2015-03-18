using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class CurrencyViewModel
    {
        [Required, Range(0, 0xf4240)]
        public decimal ConversionRateToDefault { get; set; }

        public int Id { get; set; }

        public bool IsDefaultCurrency { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required, StringLength(3)]
        public string Symbol { get; set; }
    }
}