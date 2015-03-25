using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Common.ListItemBuilders
{
    public class CurrencyListBuilder
    {
        private readonly CurrencyService _service;

        public CurrencyListBuilder(CurrencyService service)
        {
            this._service = service;
        }

        public IList<SelectListItem> Currencies()
        {
            return new List<SelectListItem>(from c in this._service.GetAll() select new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
        }

        public IList<SelectListItem> Currencies(int id)
        {
            return new List<SelectListItem>(from c in this._service.GetAll() select new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == id });
        }
    }
}