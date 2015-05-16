using System;

namespace Zakar.ViewModels
{
    public class PartnerListModel
    {
        public int ChurchId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Title { get; set; }

        public String UniqueId { get; set; }

        public int? CellId { get; set; }

        public int PCFId { get; set; }

        public string CellName { get; set; }

        public String PCFName { get; set; }

        public String ChurchName { get; set; }

        public String GroupName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public String Gender { get; set; }

        public string ZoneName { get; set; }
    }
}