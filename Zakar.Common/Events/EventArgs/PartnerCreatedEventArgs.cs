using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakar.Models;

namespace Zakar.Common.Events.EventArgs
{
    public class PartnerCreatedEventArgs : System.EventArgs
    {
        private readonly Partner _partner;

        public PartnerCreatedEventArgs(Partner p)
        {
            this._partner = p;
        }

        public Partner GetPartner
        {
            get
            {
                return this._partner;
            }
        }
    }
}
