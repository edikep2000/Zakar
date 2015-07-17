using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office.CustomUI;
using Omu.AwesomeMvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.AjaxList
{
    [Authorize]
    public class CurrencyAjaxDropDownController : Controller
    {
        private readonly CurrencyService _currencyService;

        public CurrencyAjaxDropDownController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public ActionResult GetItems(int? v)
        {
            var c = _currencyService.GetAll().Select(i => new SelectableItem(i.Id, i.Symbol, i.Id == v)).ToList();
            return Json(c);
        }
    }
}