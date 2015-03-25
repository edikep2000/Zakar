using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using WebMatrix.WebData;
using Zakar.Common.Events.EventArgs;
using Zakar.Common.Events.EventConsumers;
using Zakar.DataAccess.Service;
using Zakar.DataAccess.Utils;
using Zakar.Models;
using Zakar.ViewModels;
using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.FileHandlers
{
    public class PartnerUploadExcelFileHandler : IFileHandler
    {
        public delegate void PartnerFileProcessedEventHandler(object s, PartnerFileUploadedEventArgs e);

        public delegate void PartnerListProcessedEventHandler(object sender, PartnerListProcessedEventArgs e);

        private readonly EventConsumers _consumer;

  
        public PartnerUploadExcelFileHandler(EventConsumers consumer, ChurchService churchService, PartnerService partnerService, GroupService groupService, UserService userService)
        {
            _consumer = consumer;
            _churchService = churchService;
            _partnerService = partnerService;
            _groupService = groupService;
            _userService = userService;
            _consumer.RegisterHandlerWhenPartnerFileIsProcessed(this);
            _consumer.RegisterHandlerWhenPartnerListIsProcessed(this);
        }


        private ChurchService _churchService;
        private GroupService _groupService;
        private readonly PartnerService _partnerService;
        private readonly UserService _userService;
        public string HandleFile(string fileName, Stream fileStream)
        {
            IList<PartnerUploadObject> uploadedObject = new List<PartnerUploadObject>();
            int num = 0;
            int num2 = 0;
            var workbook = new XLWorkbook(fileStream);
            if (workbook != null)
            {
                IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
                if (worksheet != null)
                {
                    worksheet.FirstRowUsed();
                    foreach (var row in worksheet.RowsUsed().ToList())
                    {
                        if (row != null)
                        {
                            var item = new PartnerUploadObject();
                            if (row.RowBelow().CellsUsed().Count() >= 9)
                            {
                                item.Group = row.RowBelow().Cell(1).GetString();
                                item.Church = row.RowBelow().Cell(2).GetString();
                                item.Title = row.RowBelow().Cell(3).GetString();
                                item.Surname = row.RowBelow().Cell(4).GetString();
                                item.FirstName = row.RowBelow().Cell(5).GetString();
                                item.Birthday = row.RowBelow().Cell(6).GetString();
                                item.Email = row.RowBelow().Cell(7).GetString();
                                item.YookosId = row.RowBelow().Cell(8).GetString();
                                item.PhoneNumber = row.RowBelow().Cell(9).GetString();
                                uploadedObject.Add(item);
                                num++;
                            }
                            else
                            {
                                if (row.RowBelow().CellsUsed().Any())
                                {
                                    row.RowBelow().Style.Fill.SetBackgroundColor(XLColor.AmericanRose);
                                }
                                num2++;
                            }
                        }
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0L;
                    if (OnFileProcessed != null)
                    {
                        OnFileProcessed(this, new PartnerFileUploadedEventArgs(stream));
                    }
                }
            }
            return ProcessContent(uploadedObject);
        }

        public event PartnerFileProcessedEventHandler OnFileProcessed;

        public event PartnerListProcessedEventHandler OnListProcessed;

        private string ProcessContent(IList<PartnerUploadObject> uploadedObject)
        {
            IList<PartnerUploadObject> list = new List<PartnerUploadObject>();

            IList<PartnerUploadObject> list2 = new List<PartnerUploadObject>();
            int num = 0;
            int num2 = 0;
            //we are not interested in the group and all that. This code can only be executed by someone in the church admin role
            int userId = WebSecurity.CurrentUserId;
            UserProfile user = _userService.GetById(userId);
            if (user != null && user.ChurchId.HasValue)
            {
                int churchId = user.ChurchId.Value;
                if (uploadedObject != null && uploadedObject.Any())
                {
                    List<Partner> partners = _partnerService.GetAll().Where(i => i.ChurchId == churchId).ToList();
                    if (partners.Any())
                    {
                        foreach (PartnerUploadObject u in uploadedObject)
                        {
                            Partner p =
                                partners.FirstOrDefault(
                                    i =>
                                        i.Phone.Equals(u.PhoneNumber) ||
                                        i.Email.Equals(u.Email, StringComparison.InvariantCultureIgnoreCase));
                            if (p == null)
                            {
                                Partner partner = PartnerFactory.BuildNew();
                                partner.LastName = u.Surname;
                                partner.ChurchId = churchId;
                                partner.Email = u.Email;
                                partner.FirstName = u.FirstName;
                                if (u.PhoneNumber.StartsWith("234"))
                                {
                                    partner.Phone = u.PhoneNumber;
                                }
                                else if (u.PhoneNumber.StartsWith("0"))
                                {
                                    partner.Phone = "234" + u.PhoneNumber.Substring(1);
                                }
                                else if (u.PhoneNumber.StartsWith("8") && (u.PhoneNumber.Length == 10))
                                {
                                    partner.Phone = "234" + u.PhoneNumber;
                                }
                                partner.Title = u.Title;
                                partner.YookosId = u.YookosId;
                                partner.Deleted = false;
                                partner.DateDeleted = null;
                                partner.DateCreated = DateTime.Now;
                                try
                                {
                                    _partnerService.Create(partner);
                                }
                                catch (ConstraintException)
                                {
                                    num2++;
                                }
                                _consumer.OnPartnerRecordCreated(this, new PartnerCreatedEventArgs(partner));
                                num++;
                                list.Add(u);
                            }
                            else
                            {
                                num2++;
                                list2.Add(u);
                            }
                        }
                    }
                    else
                    {
                        foreach (PartnerUploadObject u in uploadedObject)
                        {
                            
                            Partner partner = PartnerFactory.BuildNew();
                            partner.LastName = u.Surname;
                            partner.ChurchId = churchId;
                            partner.Email = u.Email;
                            partner.FirstName = u.FirstName;
                            if (u.PhoneNumber.StartsWith("234"))
                            {
                                partner.Phone = u.PhoneNumber;
                            }
                            else if (u.PhoneNumber.StartsWith("0"))
                            {
                                partner.Phone = "234" + u.PhoneNumber.Substring(1);
                            }
                            else if (u.PhoneNumber.StartsWith("8") && (u.PhoneNumber.Length == 10))
                            {
                                partner.Phone = "234" + u.PhoneNumber;
                            }
                            partner.Title = u.Title;
                            partner.YookosId = u.YookosId;
                            partner.Deleted = false;
                            partner.DateDeleted = null;
                            partner.DateCreated = DateTime.Now;
                            try
                            {
                                _partnerService.Create(partner);
                            }
                            catch (ConstraintException)
                            {
                                num2++;
                            }
                            _consumer.OnPartnerRecordCreated(this, new PartnerCreatedEventArgs(partner));
                            num++;
                            list.Add(u);
                        }
                    }
                }
            }
            if (OnListProcessed != null)
            {
                var model = new PartnershipFileCreatedMailModel
                {
                    SuccessObject = list,
                    DuplicateObject = list2
                };
                var e = new PartnerListProcessedEventArgs(model);
                OnListProcessed(this, e);
            }
            return string.Format("{0} results added successfully, {1} duplicate records detected", num, num2);
        }
    }
}