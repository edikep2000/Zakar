using System.Collections.Generic;
using System.Linq;
using Zakar.DataAccess.Service;
using Zakar.ViewModels.AnalyticsModels;

namespace Zakar.Common.Builders
{
    public class PartnerAnalyticsBuilder
    {
        private readonly PartnershipService _partnershipService;

        public PartnerAnalyticsBuilder(PartnershipService partnershipService)
        {
            this._partnershipService = partnershipService;
        }

        public IEnumerable<PieChartModel> Build(int id)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where i.PartnerId == id
                group i by new { i.PartnershipArm }
                select new PieChartModel { Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))), Name = i.Key.PartnershipArm.Name } into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<PieChartModel> Build(int id, int year)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where (i.PartnerId == id) && (i.Year == year)
                group i by new { i.PartnershipArm }
                select new PieChartModel { Amount = i.Sum(k => (k.Amount * k.Currency.ConversionRateToDefault)), Name = i.Key.PartnershipArm.Name } into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<PieChartModel> Build(int id, int month, int year)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where ((i.PartnerId == id) && (i.Month == month)) && (i.Year == year)
                group i by new { i.PartnershipArm }
                select new PieChartModel { Amount = i.Sum(k => (k.Amount * k.Currency.ConversionRateToDefault)), Name = i.Key.PartnershipArm.Name } into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<LineChartModel> History(int id)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where i.PartnerId == id
                group i by new { i.Month, i.Year }
                select new LineChartModel { Month = i.Key.Month, Year = i.Key.Year, Amount = i.Sum(o => (o.Amount * o.Currency.ConversionRateToDefault)) } into i
                orderby i.Year, i.Month
                select i).ToList<LineChartModel>();
        }

        public IEnumerable<LineChartModel> History(int id, int year)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where (i.PartnerId == id) && (i.Year == year)
                group i by new { i.Month }
                select new LineChartModel { Month = i.Key.Month, Year = year, Amount = i.Sum(o => (o.Amount * o.Currency.ConversionRateToDefault)) } into i
                orderby i.Year, i.Month
                select i).ToList<LineChartModel>();
        }
    }
}