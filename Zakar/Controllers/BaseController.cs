using System.Linq;
using System.Web.Mvc;
using Telerik.OpenAccess;
using Zakar.App_Start;
using Zakar.Models;

namespace Zakar.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if ((!filterContext.IsChildAction && (filterContext.Exception == null)) && (UnitOfWork != null))
            {
                UnitOfWork.SaveChanges();
            }
        }

        protected EntitiesModel AccessContext
        {
            get { return (EntitiesModel) this.UnitOfWork; }
        }

        protected ApplicationUserManager UserStore
        {
            get { return DependencyResolver.Current.GetService<ApplicationUserManager>(); }
        }

    }
}