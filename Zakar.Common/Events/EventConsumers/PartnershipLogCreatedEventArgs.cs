using Zakar.Models;

namespace Zakar.Common.Events.EventConsumers
{
    public class PartnershipLogCreatedEventArgs : System.EventArgs
    {
        private readonly Partnership _partnership;

        public PartnershipLogCreatedEventArgs(Partnership p)
        {
            this._partnership = p;
        }

        public Partnership GetPartnership
        {
            get
            {
                return this._partnership;
            }
        }
    }
}