using System.Linq;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class PartnersRankingBuilder
    {
        private readonly ChurchService _churchService;

        private readonly CurrencyService _currencyService;

        private readonly IRepository<Partnership> _repository;

        public PartnersRankingBuilder(ChurchService churchService, CurrencyService currencyService, IRepository<Partnership> repository)
        {
            _churchService = churchService;
            _currencyService = currencyService;
            _repository = repository;
        }

        public IQueryable<PartnerPartnershipObject> GetPartnersRanking(int year)
        {
            Currency defaultCurrency = _currencyService.GetDefault();
            return (from grouping in
                from i in _repository.GetAll()
                where i.Year == year
                group i by new { i.Partner }
                select
                    new PartnerPartnershipObject
                    {
                        Partner = grouping.Key.Partner,
                        DefaultCurrencyString = defaultCurrency.Symbol,
                        DenominatedCurrencyAmount =
                            grouping.Sum((i => (i.Currency.ConversionRateToDefault * i.Amount)))
                    }
                into i
                orderby i.DenominatedCurrencyAmount descending
                select i);
        }

        public IOrderedQueryable<PartnerPartnershipObject> GetPartnersRanking(int year, int churchId)
        {
            Currency defaultCurrency;
            Church single = _churchService.GetSingle(churchId);
            if (single.DefaultCurrencyId.HasValue)
            {
                defaultCurrency = _currencyService.GetSingle(single.DefaultCurrencyId.Value);
            }
            else
            {
                defaultCurrency = _currencyService.GetDefault();
            }
            return (from grouping in
                from i in _repository.GetAll()
                where (i.Year == year) && (i.Partner.ChurchId == churchId)
                group i by new { i.Partner }
                select
                    new PartnerPartnershipObject
                    {
                        Partner = grouping.Key.Partner,
                        DefaultCurrencyString = defaultCurrency.Symbol,
                        DenominatedCurrencyAmount =
                            grouping.Sum((i => (i.Currency.ConversionRateToDefault * i.Amount)))
                    }
                into i
                orderby i.DenominatedCurrencyAmount descending
                select i);
        }
    }
}