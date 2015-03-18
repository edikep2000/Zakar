using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.ViewModels.EmailModels
{
    public class PartnershipLogFileCreatedMailModel
    {
        public IList<PartnershipLogObject> DuplicateLogObject { get; set; }

        public IList<PartnershipLogObject> ErrorLogObject { get; set; }

        public IList<PartnershipLogObject> SuccessLogObject { get; set; }
    }
}
