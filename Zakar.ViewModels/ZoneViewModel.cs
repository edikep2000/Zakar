using System;
using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class ZoneViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Display(Name = "Unique Id")]
        public String UniqueId { get; set; }
    }

    public class ZoneListModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public string UniqueId { get; set; }

        public int GCount { get; set; }
    }

    public class ZoneDetailsModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int GroupCount { get; set; }

        public int ChapterCount { get; set; }

        public int PCFCount { get; set; }

        public int PartnershipTotal { get; set; }

        public int PartnerCount { get; set; }
    }
}