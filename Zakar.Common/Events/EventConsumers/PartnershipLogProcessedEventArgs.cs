using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.Events.EventConsumers
{
    public class PartnershipLogProcessedEventArgs : System.EventArgs
    {
        private readonly PartnershipLogFileCreatedMailModel _model;

        public PartnershipLogProcessedEventArgs(PartnershipLogFileCreatedMailModel model)
        {
            this._model = model;
        }

        public PartnershipLogFileCreatedMailModel MailModel
        {
            get
            {
                return this._model;
            }
        }
    }
}