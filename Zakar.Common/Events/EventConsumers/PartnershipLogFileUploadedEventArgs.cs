using System.IO;

namespace Zakar.Common.Events.EventConsumers
{
    public class PartnershipLogFileUploadedEventArgs : System.EventArgs
    {
        private readonly Stream _workBook;

        public PartnershipLogFileUploadedEventArgs(Stream s)
        {
            this._workBook = s;
        }

        public Stream WorkBook
        {
            get
            {
                return this._workBook;
            }
        }
    }
}