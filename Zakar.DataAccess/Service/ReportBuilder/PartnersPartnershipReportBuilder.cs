using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class PartnersPartnershipReportBuilder
    {
        private readonly CurrencyService _currencyService;
        public PartnersPartnershipReportBuilder(CurrencyService currencyService, IRepository<Partnership> repository)
        {
            _currencyService = currencyService;
            _repository = repository;
        }


        private readonly IRepository<Partnership> _repository;

        public IQueryable<PartnershipArmReportObject> BuildReportForAllPartnershipsByMonthAndByYearForPartner(
            int partnersId)
        {
            _currencyService.GetDefault();
            return (from grouping in
                from o in _repository.GetAll()
                where o.PartnerId == partnersId
                group o by new { o.Month, o.Year, o.Currency }
                select
                    new PartnershipArmReportObject
                    {
                        Year = grouping.Key.Year,
                        Month = grouping.Key.Month,
                        CurrencyId = grouping.Key.Currency.Id,
                        CurrencyName = grouping.Key.Currency.Symbol,
                        PartnerId = partnersId,
                        Amount = grouping.Sum((o => o.Amount))
                    }
                into o
                orderby o.Year
                select o);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForAllPartnershipsByMonthAndByYearForPartnerWithDefault
            (int partnersId)
        {
            Currency defaultCurency = _currencyService.GetDefault();
            return (from grouping in
                _repository.GetAll().Where(o => o.PartnerId == partnersId).GroupBy(o => new {o.Month, o.Year})
                select
                    new PartnershipArmReportObject
                    {
                        Month = grouping.Key.Month,
                        Year = grouping.Key.Year,
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        PartnerId = partnersId,
                        DenominatedAmount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into o
                orderby o.Year
                select o);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByMonthAndYearForPartner(
            int partnersId)
        {
            _currencyService.GetDefault();
            return (from grouping in
                from i in _repository.GetAll()
                where i.PartnerId == partnersId
                group i by new { i.PartnershipArm, i.Year, i.Month, i.Currency }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        Month = grouping.Key.Month,
                        CurrencyName = grouping.Key.Currency.Symbol,
                        PartnerId = partnersId,
                        CurrencyId = grouping.Key.Currency.Id,
                        Amount = grouping.Sum((o => o.Amount))
                    }
                into o
                orderby o.Year, o.Month
                select o);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByMonthAndYearForPartnerInDefault(
            int partnersId)
        {
            Currency defaultCurency = _currencyService.GetDefault();
            return (from grouping in
                _repository.GetAll()
                    .Where(i => i.PartnerId == partnersId)
                    .GroupBy(i => new {i.PartnershipArm, i.Year, i.Month})
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        Month = grouping.Key.Month,
                        PartnerId = partnersId,
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        DenominatedAmount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into o
                orderby o.Year, o.Month
                select o);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmYearlyCumulativesForPartner(
            int partnersId)
        {
            _currencyService.GetDefault();
            return (from grouping in
                from o in _repository.GetAll()
                where o.PartnerId == partnersId
                group o by new { o.PartnershipArm, o.Year, o.Currency }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        CurrencyName = grouping.Key.Currency.Symbol,
                        CurrencyId = grouping.Key.Currency.Id,
                        PartnerId = partnersId,
                        Amount = grouping.Sum((o => o.Amount))
                    }
                into o
                orderby o.Year
                select o);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmYearlyCumulativesForPartnerWithDefault
            (int partnersId)
        {
            Currency defaultCurency = _currencyService.GetDefault();
            return (from grouping in
                _repository.GetAll().Where(o => o.PartnerId == partnersId).GroupBy(o => new {o.PartnershipArm, o.Year})
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        PartnerId = partnersId,
                        DenominatedAmount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into o
                orderby o.Year
                select o);
        }
    }
}