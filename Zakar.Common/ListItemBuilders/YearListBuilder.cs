using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Zakar.Common.ListItemBuilders
{
    public class YearListBuilder
    {
        public IEnumerable<SelectListItem> BuildList()
        {
            var list = new List<SelectListItem>();
            for (var i = DateTime.Now.AddYears(-5).Year; i < DateTime.Now.AddYears(1).Year; i++)
            {
                var item = new SelectListItem
                {
                    Selected = i == DateTime.Now.Year,
                    Text = i.ToString(CultureInfo.InvariantCulture),
                    Value = i.ToString(CultureInfo.InvariantCulture)
                };
                list.Add(item);
            }
            return list;
        }
    }
}