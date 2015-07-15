using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using Omu.AwesomeMvc;
using Zakar.DataAccess;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class PartnerReportsController : Controller
    {
        private readonly PartnerService _partnerService;
        private readonly CurrencyService _currencyService;
        private readonly PartnershipService _partnershipService;


        public PartnerReportsController(PartnerService partnerService, CurrencyService currencyService,
            PartnershipService partnershipService)
        {
            _partnerService = partnerService;
            _currencyService = currencyService;
            _partnershipService = partnershipService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll(int? id)
        {
            if (id.HasValue && id.Value != 0)
            {
                var p = _partnerService.GetSingle(id.Value);
                ViewBag.Title = String.Format("Partnership Records For {0} {1} {2}", p.Title, p.FirstName, p.LastName);
                return View("Index");
            }
            return View("Index");
        }

        public ActionResult BuildReportForAllPartnershipsByMonthAndByYearForPartner(GridParams g, int id)
        {
            var model = _partnershipService.GetPartnershipRecordsForPartner(id)
                .GroupBy(o => new {o.Currency, o.Year, o.Month})
                .Select(o => new PartnershipArmReportObject()
                {
                    Year = o.Key.Year,
                    Month = o.Key.Month,
                    CurrencyId = o.Key.Currency.Id,
                    CurrencyName = o.Key.Currency.Symbol,
                    PartnerId = id,
                    Amount = o.Sum((m => m.Amount))
                });
            return
                Json(
                    new GridModelBuilder<PartnershipArmReportObject>(
                        model.OrderByDescending(k => k.Year).ThenByDescending(l => l.Month), g)
                    {
                        Map = o => new
                        {
                            o.Year,
                            Month = ((MonthEnums)o.Month).ToString(),
                            o.CurrencyName,
                            o.CurrencyId,
                            o.Amount,
                        }
                    }.Build());
        }

        public ActionResult BuildReportForAllPartnershipsByMonthAndByYearForPartnerWithDefault(GridParams g, int id)
        {
            var defaultCurrency = _currencyService.GetDefault();
            var model = _partnershipService.GetPartnershipRecordsForPartner(id)
                .GroupBy(o => new { o.Year, o.Month})
                .Select(o => new PartnershipArmReportObject()
                {
                    Month = o.Key.Month,
                    Year = o.Key.Year,
                    DenominatedCurrencyName = defaultCurrency.Symbol,
                    PartnerId = id,
                    DenominatedAmount = o.Sum((m => (m.Amount * m.Currency.ConversionRateToDefault)))
                });
            return Json(new GridModelBuilder<PartnershipArmReportObject>(
                model.OrderByDescending(k => k.Year).ThenByDescending(K => K.Month), g)
            {
                Map = o => new
                {
                    Month = ((MonthEnums)o.Month).ToString(),
                    o.Year,
                    o.DenominatedAmount,
                    o.DenominatedCurrencyName,
                    o.PartnerId,
                }
            }.Build());
        }

        public ActionResult BuildReportForPartnershipArmByMonthAndYearForPartner(GridParams g, int id)
        {
            var model = _partnershipService.GetPartnershipRecordsForPartner(id)
                .GroupBy(o => new {o.PartnershipArm, o.Year, o.Month, o.Currency})
                .Select(o => new PartnershipArmReportObject()
                {
                    PartnershipArmId = o.Key.PartnershipArm.Id,
                    PartnershipArmName = o.Key.PartnershipArm.Name,
                    Year = o.Key.Year,
                    Month = o.Key.Month,
                    CurrencyName = o.Key.Currency.Symbol,
                    PartnerId = id,
                    CurrencyId = o.Key.Currency.Id,
                    Amount = o.Sum((m => m.Amount))
                });
            return
                Json(
                    new GridModelBuilder<PartnershipArmReportObject>(
                        model.OrderByDescending(j => j.Year).ThenByDescending(j => j.Month), g)
                    {
                        Map = o => new
                        {
                            o.PartnerId,
                            Month = ((MonthEnums)o.Month).ToString(),
                            o.Year,
                            o.PartnershipArmId,
                            o.PartnershipArmName,
                            o.CurrencyName,
                            o.CurrencyId,
                            o.Amount
                        }
                    }.Build());
        }

        public ActionResult BuildReportForPartnershipArmByMonthAndYearForPartnerInDefault(GridParams g, int id)
        {
            var defaultCurrency = _currencyService.GetDefault();
            var model = _partnershipService.GetPartnershipRecordsForPartner(id)
                .GroupBy(o => new {o.PartnershipArm, o.Year, o.Month})
                .Select(o => new PartnershipArmReportObject()
                {
                    PartnershipArmId = o.Key.PartnershipArm.Id,
                    PartnershipArmName = o.Key.PartnershipArm.Name,
                    Year = o.Key.Year,
                    Month = o.Key.Month,
                    PartnerId = id,
                    DenominatedCurrencyName = defaultCurrency.Symbol,
                    DenominatedAmount = o.Sum(m => (m.Amount*m.Currency.ConversionRateToDefault))
                });
            return
               Json(
                   new GridModelBuilder<PartnershipArmReportObject>(
                       model.OrderByDescending(j => j.Year).ThenByDescending(j => j.Month), g)
                   {
                       Map = o => new
                       {
                           o.PartnerId,
                           Month = ((MonthEnums)o.Month).ToString(),
                           o.Year,
                           o.PartnershipArmId,
                           o.PartnershipArmName,
                           o.DenominatedAmount,
                           o.DenominatedCurrencyName,
                       }
                   }.Build());
        }

        public ActionResult BuildReportForPartnershipArmYearlyCumulativesForPartner(GridParams g, int id)
        {
            var model = _partnershipService.GetPartnershipRecordsForPartner(id)
                .GroupBy(o => new {o.PartnershipArm, o.Year, o.Currency})
                .Select(o => new PartnershipArmReportObject()
                {
                    PartnerId = id,
                    Amount = o.Sum(k => k.Amount),
                    CurrencyId = o.Key.Currency.Id,
                    CurrencyName = o.Key.Currency.Symbol,
                    Year = o.Key.Year,
                    PartnershipArmId = o.Key.PartnershipArm.Id,
                    PartnershipArmName = o.Key.PartnershipArm.ShortFormName,
                });
            return
                    Json(
                        new GridModelBuilder<PartnershipArmReportObject>(
                            model.OrderByDescending(j => j.Year).ThenByDescending(j => j.Month), g)
                        {
                            Map = o => new
                            {
                                o.PartnerId,
                                o.Year,
                                o.PartnershipArmId,
                                o.PartnershipArmName,
                                o.CurrencyName,
                                o.CurrencyId,
                                o.Amount
                            }
                        }.Build());
        }

        public ActionResult BuildReportForPartnershipArmYearlyCumulativesForPartnerWithDefault(GridParams g, int id)
        {
            var defaultCurrency = _currencyService.GetDefault();
            var i =
                _partnershipService.GetAll()
                    .Where(o => o.PartnerId == id)
                    .GroupBy(o => new {o.PartnershipArm, o.Year})
                    .Select(o => new PartnershipArmReportObject()
                    {
                        PartnershipArmId = o.Key.PartnershipArm.Id,
                        PartnershipArmName = o.Key.PartnershipArm.ShortFormName,
                        Year = o.Key.Year,
                        DenominatedCurrencyName = defaultCurrency != null ? defaultCurrency.Symbol : "N",
                        PartnerId = id,
                        DenominatedAmount = o.Sum((m => (m.Amount*m.Currency.ConversionRateToDefault)))
                    });
            return
                Json(
                    new GridModelBuilder<PartnershipArmReportObject>(
                        i.OrderByDescending(j => j.Year).ThenByDescending(j => j.Month), g)
                    {
                        Map = o => new
                        {
                            o.PartnerId,
                            o.Year,
                            o.PartnershipArmId,
                            o.PartnershipArmName,
                            o.DenominatedAmount,
                            o.DenominatedCurrencyName
                        }
                    }.Build());
        }



    }
}