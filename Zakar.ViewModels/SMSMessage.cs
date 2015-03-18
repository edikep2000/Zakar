namespace Zakar.ViewModels
{
    public class SMSMessage : ISMSMessage
    {
        public string Body { get; set; }

        public string Recipient { get; set; }

        public string RerenceId { get; set; }

        public string Sender { get; set; }
    }
}