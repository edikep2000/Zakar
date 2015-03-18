using System.ComponentModel.DataAnnotations;

namespace Zakar.ViewModels
{
    public class ClientAccessModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}