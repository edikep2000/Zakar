using System.Text;

namespace Zakar.Common.Formatters
{
    public class PhoneNumberFormatter : IPhoneNumberFormatter
    {
        public string FormatPhoneNumber(string phoneNumber)
        {
            var builder = new StringBuilder();
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }
            if (phoneNumber.StartsWith("+"))
            {
                phoneNumber = phoneNumber.Substring(1, phoneNumber.Length - 1);
            }
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Substring(1, phoneNumber.Length - 1);
            }
            if (phoneNumber.Length == 10)
            {
                builder.Append("234");
                builder.Append(phoneNumber);
            }
            else
            {
                builder.Append(phoneNumber);
            }
            return builder.ToString();
        }
    }
}