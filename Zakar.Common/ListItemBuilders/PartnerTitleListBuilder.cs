using System.Collections.Generic;
using System.Web.Mvc;

namespace Zakar.Common.ListItemBuilders
{
    public class PartnerTitleListBuilder
    {
        public IList<SelectListItem> Titles()
        {
            var list = new List<SelectListItem>();
            var item = new SelectListItem
            {
                Text = "Deacon",
                Value = "Deacon",
                Selected = true
            };
            list.Add(item);
            var item2 = new SelectListItem
            {
                Text = "Pastor",
                Value = "Pastor"
            };
            list.Add(item2);
            var item3 = new SelectListItem
            {
                Text = "Brother",
                Value = "Brother"
            };
            list.Add(item3);
            var item4 = new SelectListItem
            {
                Text = "Sister",
                Value = "Sister"
            };
            list.Add(item4);
            var item5 = new SelectListItem
            {
                Text = "Deaconess",
                Value = "Deaconess"
            };
            list.Add(item5);
            var item6 = new SelectListItem
            {
                Text = "Reverend",
                Value = "Reverend"
            };
            list.Add(item6);
            return list;
        }
    }
}