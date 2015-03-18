using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class CellGroupViewModel
    {
        [Required]
        public int ChurchId { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}