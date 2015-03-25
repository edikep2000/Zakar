using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zakar.Common.Enums;

namespace Zakar.Common.ListItemBuilders
{
    public class MonthListBuilder
    {
        public IList<SelectListItem> Months()
        {
            return ((IList<MonthEnums>)Enum.GetValues(typeof(MonthEnums))).Select(delegate(MonthEnums c)
            {
                var item = new SelectListItem();
                item.Value = ((int)c).ToString();
                item.Text = c.ToString();
                return item;
            }).ToList();
        }

        public IList<SelectListItem> Months(int month)
        {
// ReSharper disable once SuspiciousTypeConversion.Global
            return ((IList<MonthEnums>)Enum.GetValues(typeof(MonthEnums))).Select(delegate(MonthEnums c)
            {
                var item = new SelectListItem {Value = ((int) c).ToString(), Text = c.ToString()};
                var num2 = (int)c;
                item.Selected = num2.ToString() == month.ToString();
                return item;
            }).ToList();
        }

      

    }
}