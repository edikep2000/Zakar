using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Telerik.OpenAccess;

namespace Zakar.Controllers
{
    public class BaseApiController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(
            System.Web.Http.Controllers.HttpControllerContext controllerContext,
            System.Threading.CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                HttpResponseMessage result = base.ExecuteAsync(controllerContext, cancellationToken).Result;
                _unitOfWork.SaveChanges();
                return result;
            }, cancellationToken);
        }
    }
}