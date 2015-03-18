using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Zakar.ViewModels
{
    public class UploadView
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}