using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class CellViewModel
    {
        [Required]
        public int PCFId { get; set; }

        public string UniqueId { get; set; }

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}