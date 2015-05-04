using Telerik.OpenAccess;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class StagedGroupService : Repository<StagedGroup>
    {
        public StagedGroupService(IUnitOfWork ctx) : base(ctx)
        {
        }
    }
}