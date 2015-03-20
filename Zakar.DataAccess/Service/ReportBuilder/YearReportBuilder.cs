using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class YearReportBuilder
    {
        private readonly CurrencyService _currencyService;
        private readonly IRepository<Partnership> _repository;

        public YearReportBuilder(IRepository<Partnership> repository, CurrencyService currencyService)
        {
            _repository = repository;
            _currencyService = currencyService;
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByYear(int year)
        {
            return (from grouping in
                from i in _repository.GetAll()
                where i.Year == year
                group i by new { i.PartnershipArm, i.Month, i.Currency }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        Month = grouping.Key.Month,
                        Amount = grouping.Sum((o => o.Amount)),
                        CurrencyId = grouping.Key.Currency.Id,
                        CurrencyName = grouping.Key.Currency.Symbol
                    }
                into I
                orderby I.Month, I.Amount
                select I);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByYearNonDenominated(int year)
        {
            Currency defaultCurrency = _currencyService.GetDefault();
            return (from grouping in
                from i in _repository.GetAll()
                where i.Year == year
                group i by new { i.PartnershipArm, i.Month }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        Month = grouping.Key.Month,
                        DenominatedCurrencyName = defaultCurrency.Symbol,
                        DenominatedAmount = grouping.Sum((i => (i.Amount * i.Currency.ConversionRateToDefault)))
                    }
                into I
                orderby I.Month, I.DenominatedAmount
                select I);
        }
    }
}