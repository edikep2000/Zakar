using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class PartnershipSummaryReportBuilder
    {
        private readonly ChurchService _churchService;
        private readonly IRepository<Partnership> _repository;
        private readonly CurrencyService _service;
        public PartnershipSummaryReportBuilder(CurrencyService currencyService, ChurchService churchService,
            IRepository<Partnership> repository)
        {
            _service = currencyService;
            _churchService = churchService;
            _repository = repository;
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForAllPartnershipsByMonthAndByYear(int churchId = 0)
        {
            if (churchId == 0)
            {
                var defaultCurency = _service.GetDefault();
                return (from grouping in
                    from i in _repository.GetAll() group i by new { i.Month, i.Year, i.Currency }
                    select
                        new PartnershipArmReportObject
                        {
                            Month = grouping.Key.Month,
                            Year = grouping.Key.Year,
                            CurrencyId = grouping.Key.Currency.Id,
                            CurrencyName = grouping.Key.Currency.Symbol,
                            Amount = grouping.Sum((o => o.Amount)),
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                        }
                    into i
                    orderby i.Year, i.Month
                    select i);
            }
            var single = _churchService.GetSingle(churchId);
            if ((single != null) && single.DefaultCurrencyId.HasValue)
            {
                var defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
                return (from grouping in
                    from i in _repository.GetAll()
                    where i.Partner.ChurchId == churchId
                    group i by new { i.Month, i.Year, i.Currency }
                    select
                        new PartnershipArmReportObject
                        {
                            Month = grouping.Key.Month,
                            Year = grouping.Key.Year,
                            CurrencyId = grouping.Key.Currency.Id,
                            CurrencyName = grouping.Key.Currency.Symbol,
                            Amount = grouping.Sum((o => o.Amount)),
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                        }
                    into i
                    orderby i.Year, i.Month
                    select i);
            }
            return null;
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForAllPartnershipsByMonthAndByYearWithNoCurrency(
            int churchId = 0)
        {
            Currency defaultCurency;
            if (churchId == 0)
            {
                defaultCurency = _service.GetDefault();
                return (from grouping in
                    from i in _repository.GetAll() group i by new { i.Month, i.Year }
                    select
                        new PartnershipArmReportObject
                        {
                            Month = grouping.Key.Month,
                            Year = grouping.Key.Year,
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                        }
                    into i
                    orderby i.Year, i.Month
                    select i);
            }
            var single = _churchService.GetSingle(churchId);
            defaultCurency = null;
            if ((single != null) && single.DefaultCurrencyId.HasValue)
            {
                defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
            }
            return (from grouping in
                from i in _repository.GetAll()
                where i.Partner.ChurchId == churchId
                group i by new { i.Month, i.Year }
                select
                    new PartnershipArmReportObject
                    {
                        Month = grouping.Key.Month,
                        Year = grouping.Key.Year,
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        DenominatedAmount = grouping.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into i
                orderby i.Year, i.Month
                select i);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByMonthAndYear(int year,
            int churchId = 0)
        {
            Currency defaultCurency;
            if (churchId == 0)
            {
                defaultCurency = _service.GetDefault();
                return (from grouping in
                    from i in _repository.GetAll()
                    where i.Year == year
                    group i by new { i.PartnershipArm, i.Year, i.Month, i.Currency }
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.Key.PartnershipArm.Id,
                            PartnershipArmName = grouping.Key.PartnershipArm.Name,
                            Year = grouping.Key.Year,
                            Month = grouping.Key.Month,
                            CurrencyId = grouping.Key.Currency.Id,
                            CurrencyName = grouping.Key.Currency.Symbol,
                            Amount = grouping.Sum((o => o.Amount)),
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                        }
                    into i
                    orderby i.Year, i.Month
                    select i);
            }
            Church single = _churchService.GetSingle(churchId);
            defaultCurency = null;
            if ((single != null) && single.DefaultCurrencyId.HasValue)
            {
                defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
            }
            return (from grouping in
                from i in _repository.GetAll()
                where (i.Year == year) && (i.Partner.ChurchId == churchId)
                group i by new { i.PartnershipArm, i.Year, i.Month, i.Currency }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        Month = grouping.Key.Month,
                        CurrencyId = grouping.Key.Currency.Id,
                        CurrencyName = grouping.Key.Currency.Symbol,
                        Amount = grouping.Sum((o => o.Amount)),
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        DenominatedAmount =
                            grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                    }
                into i
                orderby i.Year, i.Month
                select i);
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmByMonthAndYearWithDefault(int year,
            int churchId
                = 0)
        {
            IQueryable<PartnershipArmReportObject> queryable = null;
            if (churchId == 0)
            {
                Currency defaultCurency = _service.GetDefault();
                queryable = from i in _repository.GetAll()
                    where i.Year == year
                    group i by new { i.PartnershipArm, i.Year, i.Month }
                    into grouping
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.Key.PartnershipArm.Id,
                            PartnershipArmName = grouping.Key.PartnershipArm.Name,
                            Year = grouping.Key.Year,
                            Month = grouping.Key.Month,
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum((i => (i.Currency.ConversionRateToDefault * i.Amount)))
                        };
            }
            else
            {
                Church single = _churchService.GetSingle(churchId);
                if ((single != null) && single.DefaultCurrencyId.HasValue)
                {
                    Currency defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
                    queryable = from i in _repository.GetAll()
                        where (i.Year == year) && (i.Partner.ChurchId == churchId)
                        group i by new { i.PartnershipArm, i.Year, i.Month }
                        into grouping
                        select
                            new PartnershipArmReportObject
                            {
                                PartnershipArmId = grouping.Key.PartnershipArm.Id,
                                PartnershipArmName = grouping.Key.PartnershipArm.Name,
                                Year = grouping.Key.Year,
                                Month = grouping.Key.Month,
                                DenominatedCurrencyName = defaultCurency.Symbol,
                                DenominatedAmount =
                                    grouping.Sum((i => (i.Currency.ConversionRateToDefault * i.Amount)))
                            };
                }
            }
            if (queryable != null)
            {
                return (from i in queryable
                    orderby i.Year, i.Month
                    select i);
            }
            return null;
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmWithYearlyCumulatives(int churchId = 0)
        {
            if (churchId == 0)
            {
                Currency defaultCurency = _service.GetDefault();
                return (from grouping in
                    from i in _repository.GetAll() group i by new { i.PartnershipArm, i.Year, i.Currency }
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.Key.PartnershipArm.Id,
                            PartnershipArmName = grouping.Key.PartnershipArm.Name,
                            Year = grouping.Key.Year,
                            CurrencyId = grouping.Key.Currency.Id,
                            CurrencyName = grouping.Key.Currency.Symbol,
                            Amount = grouping.Sum((o => o.Amount)),
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                        }
                    into i
                    orderby i.Year, i.Amount
                    select i);
            }
            Church single = _churchService.GetSingle(churchId);
            if ((single != null) && single.DefaultCurrencyId.HasValue)
            {
                Currency defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
                return (from grouping in
                    from i in _repository.GetAll()
                    where i.Partner.ChurchId == churchId
                    group i by new { i.PartnershipArm, i.Year, i.Currency }
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.Key.PartnershipArm.Id,
                            PartnershipArmName = grouping.Key.PartnershipArm.Name,
                            Year = grouping.Key.Year,
                            CurrencyId = grouping.Key.Currency.Id,
                            CurrencyName = grouping.Key.Currency.Symbol,
                            Amount = grouping.Sum((o => o.Amount)),
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum(((o => o.Amount))) * grouping.Key.Currency.ConversionRateToDefault
                        }
                    into i
                    orderby i.Year, i.Amount
                    select i);
            }
            return null;
        }

        public IQueryable<PartnershipArmReportObject> BuildReportForPartnershipArmWithYearlyCumulativesWithoutCurrency(
            int churchId = 0)
        {
            Currency defaultCurency;
            if (churchId == 0)
            {
                defaultCurency = _service.GetDefault();
                return (from grouping in
                    from i in _repository.GetAll() group i by new { i.PartnershipArm, i.Year }
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.Key.PartnershipArm.Id,
                            PartnershipArmName = grouping.Key.PartnershipArm.Name,
                            Year = grouping.Key.Year,
                            DenominatedCurrencyName = defaultCurency.Symbol,
                            DenominatedAmount =
                                grouping.Sum((o => (o.Currency.ConversionRateToDefault * o.Amount)))
                        }
                    into i
                    orderby i.Year
                    select i);
            }
            Church single = _churchService.GetSingle(churchId);
            defaultCurency = null;
            if ((single != null) && single.DefaultCurrencyId.HasValue)
            {
                defaultCurency = _service.GetSingle(single.DefaultCurrencyId.Value);
            }
            return (from grouping in
                from i in _repository.GetAll()
                where i.Partner.ChurchId == churchId
                group i by new { i.PartnershipArm, i.Year }
                select
                    new PartnershipArmReportObject
                    {
                        PartnershipArmId = grouping.Key.PartnershipArm.Id,
                        PartnershipArmName = grouping.Key.PartnershipArm.Name,
                        Year = grouping.Key.Year,
                        DenominatedCurrencyName = defaultCurency.Symbol,
                        DenominatedAmount = grouping.Sum((o => (o.Currency.ConversionRateToDefault * o.Amount)))
                    }
                into i
                orderby i.Year
                select i);
        }
    }
}