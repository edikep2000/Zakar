using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.LookupControllers
{
    public class CurrencyLookupController : Controller
    {
        private readonly CurrencyService _currencyService;

        public CurrencyLookupController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
    }
}