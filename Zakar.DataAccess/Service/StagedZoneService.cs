using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
  public  class StagedZoneService : Repository<StagedZone>
  {
      public StagedZoneService(IUnitOfWork ctx) : base(ctx)
      {
      }
  }
}