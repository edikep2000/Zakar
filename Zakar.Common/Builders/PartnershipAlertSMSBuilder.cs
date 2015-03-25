using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Common.Builders
{
    public class PartnershipAlertSMSBuilder : IMessageBuilder<Partnership, string>
    {
        private readonly CurrencyService _currencyService;
        private string _fileMessage;

        public PartnershipAlertSMSBuilder(CurrencyService currencyService, PartnerService partnerRepository, PartnershipService partnershipRepository)
        {
            this._currencyService = currencyService;
            _partnerRepository = partnerRepository;
            _partnershipRepository = partnershipRepository;
            _partnershipRepository = partnershipRepository;
            var builder = new StringBuilder();
            if (HttpContext.Current.Request.PhysicalApplicationPath != null)
            {
                string str = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data");
                builder.Append(str);
            }
            builder.Append("/");
            builder.Append("smsnotificationtemplate.txt");
            this._fileMessage = File.ReadAllText(builder.ToString());
        }
        private string BuildAmount(Partnership p)
        {
            var builder = new StringBuilder();
            builder.Clear();
            builder.Append(this._currencyService.GetSingle(p.CurrencyId).Symbol);
            builder.Append(p.Amount.ToString());
            return builder.ToString();
        }
        private string BuildFullName(int id)
        {
            var builder = new StringBuilder();
            var single = this._partnerRepository.GetSingle(id);
            if (single != null)
            {
                builder.Clear();
                builder.Append(single.Title);
                builder.Append(" ");
                builder.Append(single.LastName);
                builder.Append(" ");
                builder.Append(single.FirstName);
            }
            return builder.ToString();
        }
        public string BuildMessage(Partnership messageRecipient)
        {
            if (!string.IsNullOrEmpty(this._fileMessage))
            {
                this._fileMessage = this._fileMessage.Replace("#{partner}", this.BuildFullName(messageRecipient.PartnerId));
                this._fileMessage = this._fileMessage.Replace("#{amount}", this.BuildAmount(messageRecipient));
                this._fileMessage = this._fileMessage.Replace("#{totalAmount}", this.BuildTotalAmount(messageRecipient.Partner));
            }
            return this._fileMessage;
        }
        private string BuildTotalAmount(Partner p)
        {
            IQueryable<Partnership> partnershipRecordsForPartner = this._partnershipRepository.GetPartnershipRecordsForPartner(p.Id);
            decimal num = partnershipRecordsForPartner.Sum<Partnership>(i => i.Amount);
            var partnership = partnershipRecordsForPartner.FirstOrDefault<Partnership>();
            if (partnership != null)
            {
                string symbol = partnership.Currency.Symbol;
                var builder = new StringBuilder();
                builder.Clear();
                builder.Append(symbol);
                builder.Append(num.ToString());
                return builder.ToString();
            }
            return null;
        }
        private readonly PartnerService _partnerRepository;
        private readonly PartnershipService _partnershipRepository;
    }
}