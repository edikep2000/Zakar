using System.Collections.Generic;

namespace Zakar.ViewModels.EmailModels
{
    public class PartnershipArmFileCreatedMailModel
    {
        public IList<PartnershipArmUploadedObject> DuplicateLogObject { get; set; }

        public IList<PartnershipArmUploadedObject> ErrorLogObject { get; set; }

        public IList<PartnershipArmUploadedObject> SuccessLogObject { get; set; }
    }
}