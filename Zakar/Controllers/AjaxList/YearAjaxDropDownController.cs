using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;

namespace Zakar.Controllers.AjaxList
{
    public class YearAjaxDropDownController : Controller
    {
        public ActionResult GetItems(string v)
        {
            var items = new List<string>();
            var previousyear = DateTime.Now.AddYears(-1).Year;
            var currentYear = DateTime.Now.Year;
            var newYear = DateTime.Now.AddYears(1).Year;
            items.Add(previousyear.ToString());
            items.Add(currentYear.ToString());
            items.Add(newYear.ToString());
            var s = items.Select(i => new SelectableItem
            {
                Text = i,
                Value = i,
                Selected = i == v
            });
            return Json(s);
        }
    }
}