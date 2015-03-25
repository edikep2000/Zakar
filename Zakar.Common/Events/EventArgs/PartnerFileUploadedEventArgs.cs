using System.IO;

namespace Zakar.Common.Events.EventArgs
{
    public class PartnerFileUploadedEventArgs : System.EventArgs
    {
        private readonly Stream _stream;

        public PartnerFileUploadedEventArgs(Stream s)
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