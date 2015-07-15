using Telerik.OpenAccess;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class StagedPartnershipService : Repository<StagedPartnership>
    {
        public StagedPartnershipService(IUnitOfWork work) : base(work)
        {
            
        }
    }
}