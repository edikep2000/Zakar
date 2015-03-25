using System.IO;
using System.Text;
using System.Web;
using Zakar.Models;

namespace Zakar.Common.Builders
{
    public class PartnerAlertSmsBuilder : IMessageBuilder<Partner, string>
    {
        private string _fileMessage;
        private StringBuilder _sb = new StringBuilder();

        public PartnerAlertSmsBuilder()
        {
            if (HttpContext.Current.Request.PhysicalApplicationPath != null)
            {
                string str = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data");
                this._sb.Append(str);
            }
            this._sb.Append("/");
            this._sb.Append("welcomesms.txt");
            this._fileMessage = File.ReadAllText(this._sb.ToString());
        }

        private string BuildFullName(Partner p)
        {
            this._sb = new StringBuilder();
            if (p != null)
            {
                this._sb.Clear();
                this._sb.Append(p.Title);
                this._sb.Append(" ");
                this._sb.Append(p.LastName);
                this._sb.Append(" ");
                this._sb.Append(p.FirstName);
            }
            return this._sb.ToString();
        }

        public string BuildMessage(Partner messageRecipient)
        {
            if (!string.IsNullOrEmpty(this._fileMessage))
            {
                this._fileMessage = this._fileMessage.Replace("#{partner}", this.BuildFullName(messageRecipient));
            }
            return this._fileMessage;
        }
    }
}