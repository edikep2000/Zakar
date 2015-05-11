using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Telerik.OpenAccess;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Controllers
{
    public class MobilePartnerController : BaseApiController
    {
        private readonly ChurchService _churchService;
        private readonly PartnerService _partnerService;

        public MobilePartnerController(PartnerService partnerService, ChurchService churchService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _partnerService = partnerService;
            _churchService = churchService;
        }

        public IEnumerable<String> Get()
        {
            return new[] { "value1", };
        }

        public HttpResponseMessage Post(string title, string firstName, string lastName, string email, string yookosId, string phone, int churchId = 0)
        {
            if (this._churchService.GetSingle(churchId) == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unused) { Content = new StringContent("Selected church does not exist at this time. Please try again later") };
            }
            if (this._partnerService.Exists(email, phone))
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented) { Content = new StringContent("Email and or phone number already exists in the data store") };
            }
            var entity = new Partner
            {
                DateCreated = DateTime.Now,
                ChurchId = churchId,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Title = title,
                FirstName = firstName
            };
            try
            {
                this._partnerService.Insert(entity);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Record created successully") };
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Could not create partner at this time") };
            }
        }

    }
}