using System.Web.Mvc;

namespace Zakar.Controllers
{
    [Authorize]
    public class GroupReportsController : Controller
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


        public ActionResult NoCurrencySummary(int? id)
        {
            return View();
        }


        public ActionResult NoCurrencyYearlySummary(int? id)
        {
            return View();
        }


        public ActionResult Summary(int? id)
        {
            return View();
        }


        public ActionResult YearlySummary(int? id)
        {
            return View();
        }
    }
}