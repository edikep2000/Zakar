using System;
using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class PartnershipViewModel
    {
        [Range(0, 0x3b9aca00), Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Month")]
        public int Month { get; set; }

        [Required]
        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        [Required]
        [Display(Name = "Partnership Arm")]
        public int PartnershipArmId { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }

    public class PartnershipListModel
    {
        public long Id { get; set; }

        public String TrackingId { get; set; }

        public String PartnerFullName { get; set; }


        public String PartnershipArm { get; set; }

        public String Currency { get; set; }

        public Decimal Amount { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime DateCreated { get; set; }
    }
}