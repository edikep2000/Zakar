using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;

namespace Zakar.Controllers.AjaxList
{
      [Authorize]
    public class MonthAjaxDropDownController : Controller
    {
        public ActionResult GetItems(int? v)
        {
            var monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.ToList();
            var m = new List<SelectableItem>();
            for (int i = 0; i < 12; i++)
            {
                m.Add(new SelectableItem(i +1, monthNames[i], i+1 == v));
            }
            return Json(m);
        }
    }
}