using System;
using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class PartnershipViewModel
    {
        [Range(0, 0x3b9aca00), Required]
        public decimal Amount { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int Month { get; set; }

        public PartnerViewModel Partner { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [Required]
        public int PartnershipArmId { get; set; }

        [Required]
        public int Year { get; set; }
    }
}