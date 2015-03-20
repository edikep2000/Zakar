using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service.ReportBuilder
{
    public class PartnerDrillDownReportBuilder
    {
        private readonly CurrencyService _currencyService;
        private readonly IRepository<Partnership> _repository;

        public PartnerDrillDownReportBuilder(IRepository<Partnership> repository, CurrencyService service)
        {
            _repository = repository;
            _currencyService = service;
        }

        public IQueryable<PartnershipArmReportObject> GetPartnersDrillDown(int partnerId, int armId = 0, int month = 0,
                                                                           int year = 0x7dd)
        {
            Currency defaultCurency = _currencyService.GetDefault();
            return (_repository.GetAll()
                               .Where(
                                   grouping =>
                                   (((grouping.PartnerId == partnerId) && (grouping.PartnershipArmId == armId)) &&
                                    (grouping.Month == month)) && (grouping.Year == year))
                               .Select(
                                   grouping =>
                                   new PartnershipArmReportObject
                                   {
                                       PartnershipArmId = grouping.PartnershipArmId,
                                       PartnershipArmName = grouping.PartnershipArm.Name,
                                       Year = grouping.Year,
                                       Month = grouping.Month,
                                       CurrencyName = defaultCurency.Symbol,
                                       PartnerId = partnerId,
                                       Amount = grouping.Amount * grouping.Currency.ConversionRateToDefault
                                   })
                               .OrderBy(o => o.Amount));
        }

        public IQueryable<PartnershipArmReportObject> GetPartnersDrillDown(int partnerId, int armId = 0, int month = 0,
// ReSharper disable MethodOverloadWithOptionalParameter
                                                                           int year = 0x7dd, int currencyId = 0)
// ReSharper restore MethodOverloadWithOptionalParameter
        {
            return (from grouping in _repository.GetAll()
                    where
                        ((((grouping.PartnerId == partnerId) && (grouping.PartnershipArmId == armId)) &&
                          (grouping.CurrencyId == currencyId)) && (grouping.Month == month)) && (grouping.Year == year)
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.PartnershipArmId,
                            PartnershipArmName = grouping.PartnershipArm.Name,
                            Year = grouping.Year,
                            Month = grouping.Month,
                            CurrencyName = grouping.Currency.Symbol,
                            PartnerId = partnerId,
                            Amount = grouping.Amount
                        }
                        into o
                        orderby o.Amount
                        select o);
        }

        public IOrderedQueryable<PartnershipArmReportObject> GetYearDrillDown(int partnerId, int armId, int year)
        {
            Currency defaultCurency = _currencyService.GetDefault();
            return (_repository.GetAll()
                               .Where(
                                   grouping =>
                                   ((grouping.PartnerId == partnerId) && (grouping.PartnershipArmId == armId)) &&
                                   (grouping.Year == year)).Select(grouping => new PartnershipArmReportObject
                                   {
                                       PartnershipArmId = grouping.PartnershipArmId,
                                       PartnershipArmName = grouping.PartnershipArm.Name,
                                       Year = grouping.Year,
                                       Month = grouping.Month,
                                       CurrencyName = defaultCurency.Symbol,
                                       PartnerId = partnerId,
                                       Amount = grouping.Amount * grouping.Currency.ConversionRateToDefault
                                   }).OrderBy(o => o.Amount));
        }

        public IOrderedQueryable<PartnershipArmReportObject> GetYearDrillDown(int partnerId, int armId, int year,
                                                                              int currencyId)
        {
            return (from grouping in _repository.GetAll()
                    where
                        (((grouping.PartnerId == partnerId) && (grouping.PartnershipArmId == armId)) &&
                         (grouping.CurrencyId == currencyId)) && (grouping.Year == year)
                    select
                        new PartnershipArmReportObject
                        {
                            PartnershipArmId = grouping.PartnershipArmId,
                            PartnershipArmName = grouping.PartnershipArm.Name,
                            Year = grouping.Year,
                            Month = grouping.Month,
                            CurrencyName = grouping.Currency.Symbol,
                            PartnerId = partnerId,
                            Amount = grouping.Amount
                        }
                        into o
                        orderby o.Amount
                        select o);
        }
    }
}
