using System.IO;
using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.Events.EventConsumers
{
    public class PartnerListProcessedEventArgs : System.EventArgs
    {
        private readonly PartnershipFileCreatedMailModel _model;

        public PartnerListProcessedEventArgs(PartnershipFileCreatedMailModel model)
        {
            this._model = model;
        }

        public PartnershipFileCreatedMailModel Mode
        {
            get
            {
                return this._model;
            }
        }
    }

    public class PartnershipArmListProcessedEventArgs : System.EventArgs
    {
        private readonly PartnershipArmFileCreatedMailModel _model;

        public PartnershipArmListProcessedEventArgs(PartnershipArmFileCreatedMailModel model)
        {
            this._model = model;
        }

        public PartnershipArmFileCreatedMailModel Model
        {
            get
            {
                return this._model;
            }
        }
    }

    public class PartnershipArmUploadedEventArgs : System.EventArgs
    {
        private readonly Stream _stream;

        public PartnershipArmUploadedEventArgs(Stream s)
        {
            this._stream = s;
        }

        public Stream WorkBook
        {
            get
            {
                return this._stream;
            }
        }
    }
}