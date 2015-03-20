using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class PartnershipArmDrillDownReportBuilder
    {
        private readonly CurrencyService _currencyService;
        private readonly IRepository<Partnership> _repository;

        public PartnershipArmDrillDownReportBuilder(IRepository<Partnership> repository, CurrencyService currencyService)
        {
            _repository = repository;
            _currencyService = currencyService;
        }

        public IQueryable<PartnershipArmReportObject> BuildSubReport(int armId, int month, int year)
        {
            Currency defaultCurrency = _currencyService.GetDefault();
            return (from grouping in
                from i in _repository.GetAll()
                where ((i.PartnershipArmId == armId) && (i.Month == month)) && (i.Year == year)
                group i by new { i.Partner }
                select
                    new PartnershipArmReportObject
                    {
                        Amount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault))),
                        CurrencyName = defaultCurrency.Symbol,
                        PartnerId = grouping.Key.Partner.Id,
                        PartnerName =
                            (grouping.Key.Partner.Title + " " + grouping.Key.Partner.LastName) + " " +
                            grouping.Key.Partner.FirstName
                    }
                into i
                orderby i.Amount
                select i);
        }

        public IQueryable<PartnershipArmReportObject> BuildSubReport(int armId, int month, int year, int currencyId)
        {
            Currency currency = _currencyService.GetSingle(currencyId);
            return (_repository.GetAll()
                .Where(i => (((i.PartnershipArmId == armId) && (i.Month == month)) && (i.Year == year)) &&
                            (i.CurrencyId == currencyId))
                .GroupBy(i => new {i.Partner})
                .Select(grouping => new PartnershipArmReportObject
                {
                    Amount = grouping.Sum((o => o.Amount)),
                    CurrencyId = currencyId,
                    CurrencyName = currency.Symbol,
                    PartnerId = grouping.Key.Partner.Id,
                    PartnerName =
                        (grouping.Key.Partner.Title + " " + grouping.Key.Partner.LastName) + " " +
                        grouping.Key.Partner.FirstName
                }).OrderBy(i => i.Amount));
        }

        public IQueryable<PartnershipArmReportObject> BuildYearlySubReport(int armId, int year)
        {
            Currency defaultCurrency = _currencyService.GetDefault();
            return (from grouping in
                from i in _repository.GetAll()
                where (i.PartnershipArmId == armId) && (i.Year == year)
                group i by new { i.Partner, i.Month }
                select
                    new PartnershipArmReportObject
                    {
                        Amount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault))),
                        Month = grouping.Key.Month,
                        CurrencyName = defaultCurrency.Symbol,
                        PartnerId = grouping.Key.Partner.Id,
                        PartnerName =
                            (grouping.Key.Partner.Title + " " + grouping.Key.Partner.LastName) + " " +
                            grouping.Key.Partner.FirstName
                    }
                into i
                orderby i.Amount
                select i);
        }

        public IQueryable<PartnershipArmReportObject> BuildYearlySubReport(int armId, int year, int currencyId)
        {
            Currency currency = _currencyService.GetSingle(currencyId);
            return (from grouping in
                _repository.GetAll()
                    .Where(i => ((i.PartnershipArmId == armId) && (i.Year == year)) && (i.CurrencyId == currencyId))
                    .GroupBy(i => new {i.Partner, i.Month})
                select
                    new PartnershipArmReportObject
                    {
                        Amount = grouping.Sum((o => o.Amount)),
                        CurrencyId = currencyId,
                        Month = grouping.Key.Month,
                        CurrencyName = currency.Symbol,
                        PartnerId = grouping.Key.Partner.Id,
                        PartnerName =
                            (grouping.Key.Partner.Title + " " + grouping.Key.Partner.LastName) + " " +
                            grouping.Key.Partner.FirstName
                    }
                into i
                orderby i.Amount
                select i);
        }
    }
}