using System.Web.Mvc;
using Zakar.DataAccess.Service;

namespace Zakar.Controllers.LookupControllers
{
    public class ChapterLookupController : Controller
    {
        private readonly ChurchService _churchService;

        public ChapterLookupController(ChurchService churchService)
        {
            _churchService = churchService;
        }
    }
}