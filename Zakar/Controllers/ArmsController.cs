using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class ArmsController : ApiController
    {
        private readonly PartnershipArmService _partnershipArmPersistenceService;

        public ArmsController(PartnershipArmService partnershipArmPersistenceService)
        {
            this._partnershipArmPersistenceService = partnershipArmPersistenceService;
        }

        public IEnumerable<PartnershipArmViewModel> Get()
        {
            return (from i in this._partnershipArmPersistenceService.GetAll()
                select new PartnershipArmViewModel { Name = i.Name, ShortFormName = i.ShortFormName, Id = i.Id } into i
                orderby i.Name
                select i).AsEnumerable<PartnershipArmViewModel>();
        }
    }
}