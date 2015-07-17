using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Controllers.GridController
{
      [Authorize]
    public class PartnershipReportsGridController : Controller
    {
        private readonly PartnershipService _partnershipService;
        private readonly CurrencyService _correncyService;

        public PartnershipReportsGridController(PartnershipService partnershipService, CurrencyService correncyService)
        {
            _partnershipService = partnershipService;
            _correncyService = correncyService;
        }

        public ActionResult BuildReportForPartnershipArmWithYearlyCumulatives(GridParams g, int? zoneId, int? groupId, int? churchId)
        {
            var defaultCurrency = _correncyService.GetDefault();
            var m = _partnershipService.GetAll();
            m = zoneId.HasValue ? m.Where(i => i.Partner.Church.Group.ZoneId == zoneId) : m;
            m = groupId.HasValue ? m.Where(i => i.Partner.Church.GroupId == groupId) : m;
            m = churchId.HasValue ? m.Where(i => i.Partner.ChurchId == churchId) : m;
            var model = m.GroupBy(i => new {i.PartnershipArm, i.Currency, i.Year}).Select(
                j => new PartnershipArmReportObject()
                {
                    PartnershipArmId = j.Key.PartnershipArm.Id,
                    PartnershipArmName = j.Key.PartnershipArm.Name,
                    Year = j.Key.Year,
                    CurrencyId = j.Key.Currency.Id,
                    CurrencyName = j.Key.Currency.Symbol, 
                    Amount = j.Sum(o => o.Amount),
                    DenominatedCurrencyName = defaultCurrency.Symbol,
                    DenominatedAmount = j.Sum(((o => o.Amount))) * j.Key.Currency.ConversionRateToDefault
                });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(model.OrderByDescending(k => k.Year), g)
            {
                Map = o => new
                {
                    o.PartnershipArmId,
                    o.PartnershipArmName,
                    o.Year,
                    o.CurrencyId,
                    o.CurrencyName,
                    o.Amount,
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName
                }
            }.Build());

        }


        public ActionResult BuildReportForPartnershipArmByMonthAndYear(GridParams g, int? year, int? zoneId, int? groupId,
            int? churchId)
        {
            var defaultCurrency = _correncyService.GetDefault();
            year = year.HasValue ? year.Value : DateTime.Now.Year;
            var m = _partnershipService.GetAll().Where(i => i.Year == year);
            m = zoneId.HasValue ? m.Where(i => i.Partner.Church.Group.ZoneId == zoneId) : m;
            m = groupId.HasValue ? m.Where(i => i.Partner.Church.GroupId == groupId) : m;
            m = churchId.HasValue ? m.Where(i => i.Partner.ChurchId == churchId) : m;
            var model = m.GroupBy(i => new { i.PartnershipArm, i.Currency, i.Year, i.Month }).Select(
               j => new PartnershipArmReportObject()
               {
                   PartnershipArmId = j.Key.PartnershipArm.Id,
                   PartnershipArmName = j.Key.PartnershipArm.Name,
                   Year = j.Key.Year,
                   Month = j.Key.Month,
                   CurrencyId = j.Key.Currency.Id,
                   CurrencyName = j.Key.Currency.Symbol,
                   Amount = j.Sum(o => o.Amount),
                   DenominatedCurrencyName = defaultCurrency.Symbol,
                   DenominatedAmount = j.Sum(((o => o.Amount))) * j.Key.Currency.ConversionRateToDefault
               });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(model.OrderByDescending(k => k.Year), g)
            {
                Map = o => new
                {
                    o.PartnershipArmId,
                    o.PartnershipArmName,
                    o.Year,
                    Month = ((MonthEnums)o.Month).ToString(),
                    o.CurrencyId,
                    o.CurrencyName,
                    o.Amount,
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName
                }
            }.Build());

        }

        public ActionResult BuildReportForPartnershipArmWithYearlyCumulativesWithoutCurrency(GridParams g, int? zoneId, int? groupId,
            int? churchId)
        {
            var defaultCurrency = _correncyService.GetDefault();
            var m = _partnershipService.GetAll();
            m = zoneId.HasValue ? m.Where(i => i.Partner.Church.Group.ZoneId == zoneId) : m;
            m = groupId.HasValue ? m.Where(i => i.Partner.Church.GroupId == groupId) : m;
            m = churchId.HasValue ? m.Where(i => i.Partner.ChurchId == churchId) : m;
            var model = m.GroupBy(i => new {i.PartnershipArm, i.Year}).Select(
                j => new PartnershipArmReportObject()
                {
                    PartnershipArmId = j.Key.PartnershipArm.Id,
                    PartnershipArmName = j.Key.PartnershipArm.Name,
                    Year = j.Key.Year,
                    DenominatedCurrencyName = defaultCurrency.Symbol,
                    DenominatedAmount = j.Sum((o => (o.Currency.ConversionRateToDefault*o.Amount)))
                });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(model.OrderByDescending(k => k.Year), g)
            {
                Map = o => new
                {
                    o.PartnershipArmId,
                    o.PartnershipArmName,
                    o.Year,
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName
                }
            }.Build());
        }

        public ActionResult BuildReportForPartnershipArmByMonthAndYearWithDefault(GridParams g, int? year, int? zoneId, int? groupId,
            int? churchId)
        {
            var defaultCurrency = _correncyService.GetDefault();
            year = year.HasValue ? year.Value : DateTime.Now.Year;
            var m = _partnershipService.GetAll().Where(i => i.Year == year);
            m = zoneId.HasValue ? m.Where(i => i.Partner.Church.Group.ZoneId == zoneId) : m;
            m = groupId.HasValue ? m.Where(i => i.Partner.Church.GroupId == groupId) : m;
            m = churchId.HasValue ? m.Where(i => i.Partner.ChurchId == churchId) : m;
            var model = m.GroupBy(i => new { i.PartnershipArm, i.Year, i.Month }).Select(
                j => new PartnershipArmReportObject()
                {
                    PartnershipArmId = j.Key.PartnershipArm.Id,
                    PartnershipArmName = j.Key.PartnershipArm.Name,
                    Year = j.Key.Year,
                    Month = j.Key.Month,
                    DenominatedCurrencyName = defaultCurrency.Symbol,
                    DenominatedAmount = j.Sum((o => (o.Currency.ConversionRateToDefault * o.Amount)))
                });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(model.OrderByDescending(k => k.Year), g)
            {
                Map = o => new
                {
                    o.PartnershipArmId,
                    o.PartnershipArmName,
                    o.Year,
                    Month = ((MonthEnums)o.Month).ToString(),
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName
                }
            }.Build());
        }

        public ActionResult BuildReportForAllPartnershipsByMonthAndByYearWithNoCurrency(GridParams g, int? zoneId, int? groupId,
            int? churchId)
        {
            var defaultCurrency = _correncyService.GetDefault();
            var m = _partnershipService.GetAll();
            m = zoneId.HasValue ? m.Where(i => i.Partner.Church.Group.ZoneId == zoneId) : m;
            m = groupId.HasValue ? m.Where(i => i.Partner.Church.GroupId == groupId) : m;
            m = churchId.HasValue ? m.Where(i => i.Partner.ChurchId == churchId) : m;
            var model = m.GroupBy(i => new { i.Year, i.Month }).Select(
                 j => new PartnershipArmReportObject()
                 {
                    Year = j.Key.Year,
                     Month = j.Key.Month,
                     DenominatedCurrencyName = defaultCurrency.Symbol,
                     DenominatedAmount = j.Sum((o => (o.Currency.ConversionRateToDefault * o.Amount)))
                 });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(model.OrderByDescending(k => k.Year), g)
            {
                Map = o => new
                {
                    o.Year,
                    Month = ((MonthEnums)o.Month).ToString(),
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName
                }
            }.Build());
        }
    }
}