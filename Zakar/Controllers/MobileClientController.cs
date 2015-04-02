using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Telerik.OpenAccess;
using Zakar.Common.Formatters;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class MobileClientController : BaseApiController
    {
        private readonly CurrencyService _currencyService;
        private readonly PartnerService _partnerService;
        private readonly PartnershipArmService _partnershipArmService;
        private readonly PartnershipService _partnershipService;
        private readonly IPhoneNumberFormatter _phoneNumberFormatter;
        private readonly NonValidatedRecordsPersistenceService _recordsPersistenceService;

        public MobileClientController(IPhoneNumberFormatter phoneNumberFormatter, PartnershipArmService partnershipArmService,
            PartnerService partnerService, NonValidatedRecordsPersistenceService recordsPersistenceService, CurrencyService currencyService,
            PartnershipService partnershipService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _phoneNumberFormatter = phoneNumberFormatter;
            _partnershipArmService = partnershipArmService;
            _partnerService = partnerService;
            _recordsPersistenceService = recordsPersistenceService;
            _currencyService = currencyService;
            _partnershipService = partnershipService;
        }



        public List<ValidatedPartnershipRecordViewModel> Get(string email, string phone)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(phone))
            {
                return (from i in this._partnershipService.GetByEmailAndPhone(email, phone)
                    select new ValidatedPartnershipRecordViewModel { Amount = i.Amount, Month = (MonthEnums)i.Month, PartnershipArm = i.PartnershipArm.Name, Year = i.Year, Currency = i.Currency.Symbol } into i
                    orderby i.Year descending, i.Month
                    select i).ToList<ValidatedPartnershipRecordViewModel>().ToList<ValidatedPartnershipRecordViewModel>();
            }
            return new List<ValidatedPartnershipRecordViewModel>();
        }

        public HttpResponseMessage Post(string email, string phone, int month, int year, string arm, decimal amount)
        {
            Partner partnerByEmailAndPhone = this._partnerService.GetPartnerByEmailAndPhone(email, phone);
            if (partnerByEmailAndPhone == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Partner with credentials does not exist.") };
            }
            PartnershipArm single = this._partnershipArmService.GetSingle(arm);
            if (single == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Selected partnership arm does not exist at this time.") };
            }
            Currency currency = this._currencyService.GetDefault();
            if (currency == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Partner with credentials does not exist.") };
            }
            var record = new NonValidatedPartnershipRecord
            {
                Amount = amount,
                Currency = currency.Id,
                DateCreated = DateTime.Now,
                Partner = partnerByEmailAndPhone.Id,
                Month = month,
                PartnershipArm = single.Id,
                Year = year
            };
            try
            {
                this._recordsPersistenceService.Create(record);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Record Logged successfully") };
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Error saving record! Try again later") };
            }
        }
    }
}