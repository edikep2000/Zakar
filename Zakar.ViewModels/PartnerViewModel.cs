using System;
using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class PartnerViewModel
    {
        public int ChurchId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [StringLength(100), DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Key]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string LastName { get; set; }

        [Required, DataType(DataType.PhoneNumber), StringLength(20)]
        public string Phone { get; set; }

        [Required, StringLength(10)]
        public string Title { get; set; }

        [Display(Name = "Partner Id")]
        public String UniqueId { get; set; }

        [Display(Name = "Cell")]
        public int CellId { get; set; }

        [Display(Name = "PCF")]
        public int PCFId { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public String Gender { get; set; }
    }
}