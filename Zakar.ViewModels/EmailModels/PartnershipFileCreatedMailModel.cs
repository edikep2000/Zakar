using System.Collections.Generic;

namespace Zakar.ViewModels.EmailModels
{
    public class PartnershipFileCreatedMailModel
    {
        public IList<PartnerUploadObject> DuplicateObject { get; set; }

        public IList<PartnerUploadObject> ErrorObject { get; set; }

        public IList<PartnerUploadObject> SuccessObject { get; set; }
    }
}