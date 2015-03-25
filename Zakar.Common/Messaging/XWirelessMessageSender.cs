using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Zakar.Common.Formatters;
using Zakar.ViewModels;

namespace Zakar.Common.Messaging
{
    public class XWirelessMessageSender : IMessageSender
    {
        private const string FormatParameterName = "format";
        private const string FormatParameterValue = "json";
        private const string InternationalParameterName = "international";
        private const string MessageParameterName = "message";
        private const string MethodParameterName = "method";
        private const string MethodParameterValue = "compose";
        private const string PasswordParametmerValue = "Udoji22";
        private const string PaswwordParameterName = "password";
        private const string RecipeintParameterName = "to";
        private const string SenderParameterName = "sender";
        private const string SMSServiceUrl = "http://smsc.xwireless.net/API/WebSMS/Http/v2.0/";
        private const string UsernameParameterName = "username";
        private const string UsernameParameterValue = "xanosms.com";


        public XWirelessMessageSender(IPhoneNumberFormatter formatter)
        {
            _formatter = formatter;
        }

        public string SendMessage(ISMSMessage message)
        {
            var message2 = message as SMSMessage;
            if (message2 != null)
            {
                return this.SendMessage(message2);
            }
            return string.Empty;
        }

        public string SendMessage(SMSMessage message)
        {
            Uri address = new Uri("http://smsc.xwireless.net/API/WebSMS/Http/v2.0/", UriKind.Absolute);
            var data = new NameValueCollection();
            data.Add("method", "compose");
            data.Add("username", "xanosms.com");
            data.Add("password", "Udoji22");
            data.Add("sender", "Zakar");
            data.Add("to", message.Recipient);
            data.Add("message", message.Body);
            data.Add("international", "1");
            data.Add("format", "json");
            using (var client = new WebClient())
            {
                byte[] bytes = client.UploadValues(address, "POST", data);
                return new UTF8Encoding().GetString(bytes);
            }
        }

        private readonly IPhoneNumberFormatter _formatter;
    }
}

