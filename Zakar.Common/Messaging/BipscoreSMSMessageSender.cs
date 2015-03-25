using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Configuration;
using Zakar.Common.Formatters;
using Zakar.ViewModels;

namespace Zakar.Common.Messaging
{
    public class BipscoreSMSMessageSender : IMessageSender
    {
        private readonly IPhoneNumberFormatter _formatter;

        public BipscoreSMSMessageSender(IPhoneNumberFormatter formatter)
        {
            _formatter = formatter;
        }

        public string SendMessage(ISMSMessage m)
        {
            var message = (SMSMessage)m;
            var builder = new StringBuilder();
            string[] strArray = WebConfigurationManager.AppSettings.GetValues("SILVERSTREET_SMSServiceURL");
            if (strArray != null)
            {
                builder.Append(strArray[0]);
            }
            var data = new NameValueCollection();
            string[] values = WebConfigurationManager.AppSettings.GetValues("SILVERSTREET_SMSServiceUsername");
            if (values != null)
            {
                data.Add("username", values[0]);
            }
            string[] strArray3 = WebConfigurationManager.AppSettings.GetValues("SILVERSTREET_SMSServicePassword");
            if (strArray3 != null)
            {
                data.Add("password", strArray3[0]);
            }
            data.Add("destination", _formatter.FormatPhoneNumber(message.Recipient));
            string[] strArray4 = WebConfigurationManager.AppSettings.GetValues("SILVERSTREET_SMSDefaultSender");
            if (strArray4 != null)
            {
                data.Add("sender", strArray4[0]);
            }
            data.Add("body", message.Body);
            using (var client = new WebClient())
            {
                byte[] bytes = client.UploadValues(new Uri(builder.ToString()), "POST", data);
                return new UTF8Encoding().GetString(bytes);
            }
        }
    }
}