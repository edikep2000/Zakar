using System.Collections.Generic;
using System.Linq;
using Zakar.DataAccess.Service;
using Zakar.ViewModels.AnalyticsModels;

namespace Zakar.Common.Builders
{
    public class PartnershipAnalyticsBuilder
    {
        private readonly PartnershipService _partnershipService;

        public PartnershipAnalyticsBuilder(PartnershipService partnershipService)
        {
            _partnershipService = partnershipService;
        }

        public IEnumerable<PieChartModel> Build(int churchId = 0)
        {
            if (churchId == 0)
            {
                return (from i in
                    from i in _partnershipService.GetAll() group i by new { i.PartnershipArm }
                    select
                        new PieChartModel
                        {
                            Amount = i.Sum(k => (k.Amount * k.Currency.ConversionRateToDefault)),
                            Name = i.Key.PartnershipArm.Name
                        }
                    into i
                    orderby i.Amount
                    select i).ToList<PieChartModel>();
            }
            return (from i in
                from i in _partnershipService.GetAll()
                where i.Partner.ChurchId == churchId
                group i by new { i.PartnershipArm }
                select
                    new PieChartModel
                    {
                        Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                        Name = i.Key.PartnershipArm.Name
                    }
                into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<LineChartModel> BuildForArm(int id, int churchId = 0)
        {
            if (churchId == 0)
            {
                return (from i in
                    from i in _partnershipService.GetAll()
                    where i.PartnershipArm.Id == id
                    group i by new { i.Month, i.Year }
                    select
                        new LineChartModel
                        {
                            Month = i.Key.Month,
                            Year = i.Key.Year,
                            Amount = i.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                        }
                    into i
                    orderby i.Year, i.Month
                    select i).ToList<LineChartModel>();
            }
            return (from i in
                from i in _partnershipService.GetAll()
                where (i.PartnershipArm.Id == id) && (i.Partner.ChurchId == churchId)
                group i by new { i.Month, i.Year }
                select
                    new LineChartModel
                    {
                        Month = i.Key.Month,
                        Year = i.Key.Year,
                        Amount = i.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into i
                orderby i.Year, i.Month
                select i).ToList<LineChartModel>();
        }

        public IEnumerable<LineChartModel> BuildForArmWithYear(int id, int year, int churchId = 0)
        {
            if (churchId == 0)
            {
                return (from i in
                    from i in _partnershipService.GetAll()
                    where (i.PartnershipArm.Id == id) && (i.Year == year)
                    group i by new { i.Month, i.Year }
                    select
                        new LineChartModel
                        {
                            Month = i.Key.Month,
                            Year = i.Key.Year,
                            Amount = i.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                        }
                    into i
                    orderby i.Year, i.Month
                    select i).ToList<LineChartModel>();
            }
            return (from i in
                from i in _partnershipService.GetAll()
                where ((i.PartnershipArm.Id == id) && (i.Year == year)) && (i.Partner.ChurchId == churchId)
                group i by new { i.Month, i.Year }
                select
                    new LineChartModel
                    {
                        Month = i.Key.Month,
                        Year = i.Key.Year,
                        Amount = i.Sum((o => (o.Amount * o.Currency.ConversionRateToDefault)))
                    }
                into i
                orderby i.Year, i.Month
                select i).ToList<LineChartModel>();
        }

        public IEnumerable<PieChartModel> BuildForChurch()
        {
            return (from i in
                from i in _partnershipService.GetAll() group i by new { i.Partner.Church }
                select
                    new PieChartModel
                    {
                        Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                        Name = i.Key.Church.Name
                    }
                into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<PieChartModel> BuildForChurch(int year)
        {
            return (from i in
                from i in _partnershipService.GetAll()
                where i.Year == year
                group i by new { i.Partner.Church }
                select
                    new PieChartModel
                    {
                        Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                        Name = i.Key.Church.Name
                    }
                into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<PieChartModel> BuildForMonthAndYear(int month, int year, int churchId = 0)
        {
            if (churchId == 0)
            {
                return (from i in
                    from i in _partnershipService.GetAll()
                    where (i.Month == month) && (i.Year == year)
                    group i by new { i.PartnershipArm }
                    select
                        new PieChartModel
                        {
                            Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                            Name = i.Key.PartnershipArm.Name
                        }
                    into i
                    orderby i.Amount
                    select i).ToList<PieChartModel>();
            }
            return (from i in
                from i in _partnershipService.GetAll()
                where ((i.Month == month) && (i.Year == year)) && (i.Partner.ChurchId == churchId)
                group i by new { i.PartnershipArm }
                select
                    new PieChartModel
                    {
                        Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                        Name = i.Key.PartnershipArm.Name
                    }
                into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }

        public IEnumerable<PieChartModel> BuildYear(int year, int churchId = 0)
        {
            if (churchId == 0)
            {
                return (from i in
                    from i in _partnershipService.GetAll()
                    where i.Year == year
                    group i by new { i.PartnershipArm }
                    select
                        new PieChartModel
                        {
                            Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                            Name = i.Key.PartnershipArm.Name
                        }
                    into i
                    orderby i.Amount
                    select i).ToList<PieChartModel>();
            }
            return (from i in
                from i in _partnershipService.GetAll()
                where (i.Year == year) && (i.Partner.ChurchId == churchId)
                group i by new { i.PartnershipArm }
                select
                    new PieChartModel
                    {
                        Amount = i.Sum((k => (k.Amount * k.Currency.ConversionRateToDefault))),
                        Name = i.Key.PartnershipArm.Name
                    }
                into i
                orderby i.Amount
                select i).ToList<PieChartModel>();
        }
    }
}