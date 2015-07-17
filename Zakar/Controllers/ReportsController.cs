using System.Web.Mvc;

namespace Zakar.Controllers
{
    [Authorize]
    public class ReportsController : Controller
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


        public ActionResult NoCurrencySummary()
        {
            return View();
        }


        public ActionResult NoCurrencyYearlySummary()
        {
            return View();
        }


        public ActionResult Summary()
        {
            return View();
        }


        public ActionResult YearlySummary()
        {
            return View();
        }
    }
}