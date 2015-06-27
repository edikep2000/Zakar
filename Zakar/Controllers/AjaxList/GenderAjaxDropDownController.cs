using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;

namespace Zakar.Controllers.AjaxList
{
    public class GenderAjaxDropDownController : Controller
    {
        public ActionResult GetItems(string v)
        {
            var s = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Selected = true,
                    Text = "Male",
                    Value = "Male"
                },
                new SelectListItem
                {
                    Selected = false,
                    Text = "Female",
                    Value = "Female"
                }
            }.Select(i => new SelectableItem()
            {
                Selected = i.Value == v,
                Value = i.Value,
                Text = i.Text
            }).ToList();
            s.Add(new SelectableItem(null, "Select Gender", string.IsNullOrWhiteSpace(v)));
            return Json(s);
        }
    }
}