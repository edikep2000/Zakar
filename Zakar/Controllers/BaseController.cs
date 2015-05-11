using System.Linq;
using System.Web.Mvc;
using Telerik.OpenAccess;

namespace Zakar.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        protected BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if ((!filterContext.IsChildAction && (filterContext.Exception == null)) && (_unitOfWork != null))
            {
                _unitOfWork.SaveChanges();
            }
        }

    }
}