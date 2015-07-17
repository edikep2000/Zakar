using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;

namespace Zakar.Controllers
{
    [RoutePrefix("church-reports")]
    [Authorize]
    public class ChurchReportsController : Controller
    {
        // GET: ChurchReports
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult NoCurrencyAmountSummary()
        {
            return View();
        }

        [Route("summary-local-currency/{id:int?}")]
        public ActionResult NoCurrencySummary(int? id)
        {
            return View();
        }

        [Route("yearly-summary-local-currency/{id:int?}")]
        public ActionResult NoCurrencyYearlySummary(int? id)
        {
            return View();
        }

        [Route("all-summary/{id:int?}")]
        public ActionResult Summary(int? id)
        {
            return View();
        }


        [Route("all-yearly-summary/{id:int?}")]
        public ActionResult YearlySummary(int? id)
        {
            return View();
        }

    }
}