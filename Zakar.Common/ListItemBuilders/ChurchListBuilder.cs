using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Common.ListItemBuilders
{
    public class ChurchListBuilder
    {
        private readonly ChurchService _churchService;

        public ChurchListBuilder(ChurchService churchService)
        {
            this._churchService = churchService;
        }

        public IEnumerable<SelectListItem> Churches()
        {
            return (from i in this._churchService.GetAll().ToList() select new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).AsEnumerable<SelectListItem>();
        }

        public IEnumerable<SelectListItem> Churches(int churchId)
        {
            return (from i in this._churchService.GetAll().ToList() select new SelectListItem { Text = i.Name, Value = i.Id.ToString(), Selected = i.Id == churchId }).AsEnumerable<SelectListItem>();
        }
    }
}
