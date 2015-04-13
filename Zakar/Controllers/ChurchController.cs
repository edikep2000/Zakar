using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zakar.DataAccess.Service;
using Zakar.ViewModels;

namespace Zakar.Controllers
{
    public class ChurchController : ApiController
    {
        private readonly ChurchService _churchService;

        public ChurchController(ChurchService churchService)
        {
            _churchService = churchService;
        }

        public List<ChurchViewModel> Get()
        {
            return (from i in _churchService.GetAll()
                select new ChurchViewModel { GroupId = i.GroupId, Id = i.Id, Name = i.Name } into i
                orderby i.Name
                select i).ToList<ChurchViewModel>();
        }

        public List<ChurchViewModel> Get(int id)
        {
            return (from i in _churchService.GetAll()
                where i.GroupId == id
                select new ChurchViewModel { GroupId = i.GroupId, Id = i.Id, Name = i.Name } into i
                orderby i.Name
                select i).ToList<ChurchViewModel>();
        }
    }
}