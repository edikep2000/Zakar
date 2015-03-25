using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Common.ListItemBuilders
{
    public class PartnershipArmListBuilder
    {
        private readonly PartnershipArmService _service;

        public PartnershipArmListBuilder(PartnershipArmService service)
        {
            this._service = service;
        }

        public IList<SelectListItem> PartnershipArms()
        {
            return new List<SelectListItem>(from c in this._service.GetAll() select new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
        }

        public IList<SelectListItem> PartnershipArms(int partnershipArmId)
        {
            return new List<SelectListItem>(from c in this._service.GetAll() select new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == partnershipArmId });
        }
    }
}