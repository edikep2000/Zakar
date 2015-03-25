using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using WebMatrix.WebData;
using Zakar.Common.Events.EventConsumers;
using Zakar.DataAccess.Service;
using Zakar.DataAccess.Utils;
using Zakar.Models;
using Zakar.ViewModels;
using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.FileHandlers
{
    public class PartnershipLogExcelFileHandler : IFileHandler
    {
        public delegate void PartnerFileProcessedEventHandler(object s, PartnershipLogFileUploadedEventArgs e);
        public delegate void PartnershipListProcessedEventHandler(object sender, PartnershipLogProcessedEventArgs e);
        public delegate void PartnershipRecordAddedEventHandler(object sender, PartnershipLogCreatedEventArgs e);
        public PartnershipLogExcelFileHandler(EventConsumers consumer, CurrencyService currencyService, PartnerService partnerService, PartnershipService partnershipService, PartnershipArmService service, UserService userService)
        {
            var consumer1 = consumer;
            _currencyService = currencyService;
            _partnerService = partnerService;
            _partnershipService = partnershipService;
            _service = service;
            _userService = userService;
            consumer1.RegisterHandlerWhenPartnershipLogFileUploaded(this);
            consumer1.RegisterHandlerWhenPartnershipLogRecordedIsCreated(this);
            consumer1.RegisterHandlerWhenPartnershipLogListIsProcessed(this);
        }


        private readonly CurrencyService _currencyService;


        private readonly PartnerService _partnerService;


        private readonly PartnershipService _partnershipService;


        private readonly PartnershipArmService _service;


        private readonly UserService _userService;

        public string HandleFile(string fileName, Stream fileStream)
        {
            IList<PartnershipLogObject> record = new List<PartnershipLogObject>();
            var workbook = new XLWorkbook(fileStream);
            IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet != null)
            {
                IXLRow row = worksheet.FirstRowUsed();
                worksheet.FirstColumnUsed();
                IXLRow row2 = row.RowBelow();
                using (var enumerator = worksheet.RowsUsed().ToList().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (row2 != null)
                        {
                            var item = new PartnershipLogObject();
                            if (row2.CellsUsed().Count() >= 6)
                            {
                                item.Email = row2.Cell(1).GetString();
                                item.Phone = row2.Cell(2).GetString();
                                if (!string.IsNullOrEmpty(item.Phone))
                                {
                                    if (item.Phone.Length == 10)
                                    {
                                        item.Phone = "234" + item.Phone;
                                    }
                                    else if (item.Phone.StartsWith("0") && (item.Phone.Length == 11))
                                    {
                                        item.Phone = "234" + item.Phone.Substring(1);
                                    }
                                    else if (!item.Phone.StartsWith("0"))
                                    {
                                        item.Phone = "234" + item.Phone;
                                    }
                                }
                                item.PartnershipArm = row2.Cell(3).GetString();
                                item.Month = row2.Cell(4).GetString();
                                item.year = row2.Cell(5).GetValue<int>();
                                item.Currency = row2.Cell(6).GetString();
                                item.Amount = (decimal) row2.Cell(7).GetDouble();
                                record.Add(item);
                            }
                            else if (row2.CellsUsed().Any())
                            {
                                row2.Style.Fill.SetBackgroundColor(XLColor.Red);
                            }
                        }
                        if (row2 != null)
                        {
                            row2 = row2.RowBelow();
                        }
                    }
                }
            }
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0L;
                if (OnProcessed != null)
                {
                    OnProcessed(this, new PartnershipLogFileUploadedEventArgs(stream));
                }
            }
            return ProcessContent(record);
        }

        public event PartnershipRecordAddedEventHandler OnAdded;

        public event PartnershipListProcessedEventHandler OnListCompleted;

        public event PartnerFileProcessedEventHandler OnProcessed;

        private string ProcessContent(IList<PartnershipLogObject> record)
        {
            const string format = "{0} results added successfully, {1} duplicate records detected, {2} errors occurred. Detailed reports have been sent to your Email Address";
            IList<PartnershipLogObject> list = new List<PartnershipLogObject>();
            IList<PartnershipLogObject> list2 = new List<PartnershipLogObject>();
            IList<PartnershipLogObject> list3 = new List<PartnershipLogObject>();
            if ((record == null) || !record.Any())
            {
                return String.Format(format, 0, 0, 0);
            }
            UserProfile user = _userService.GetById(WebSecurity.CurrentUserId);
            if (user != null && user.ChurchId != 0)
            {
                List<PartnershipArm> all = _service.GetAll().ToList();
                IQueryable<Currency> queryable3 = _currencyService.GetAll();

                int num = 0;
                int num3 = 0;
                int num2 = 0;
                if (user.ChurchId.HasValue)
                {
                    var source = _partnerService.GetForChurch(user.ChurchId.Value);
                    var queryable4 = _partnershipService.GetForChurch(user.ChurchId.Value);

                    num = 0;
                    num2 = 0;
                    num3 = 0;
                    using (IEnumerator<PartnershipLogObject> enumerator = record.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            var item = enumerator.Current;
                            var partner =
                                source.FirstOrDefault(
                                    i =>
                                        i.Email.Equals(item.Email, StringComparison.CurrentCultureIgnoreCase) ||
                                        i.Phone.Equals(item.Phone));
                            if (partner != null)
                            {
                                var arm =
                                    all.FirstOrDefault(
                                        i =>
                                            i.ShortFormName.Equals(item.PartnershipArm,
                                                StringComparison.CurrentCultureIgnoreCase));
                                var currency =
                                    queryable3.FirstOrDefault(i => (i.Name == item.Currency) || (i.Symbol == item.Currency));
                                var month = (int) Enum.Parse(typeof (MonthEnums), item.Month.ToUpper());
                                var entity = (from i in queryable4
                                    where
                                        (((((i.PartnerId == partner.Id) && (i.Month == month)) &&
                                           (i.Year == item.year)) &&
                                          (i.PartnershipArm.ShortFormName == item.PartnershipArm)) &&
                                         (i.Amount == item.Amount)) &&
                                        (Equals(i.Currency.Symbol, item.Currency) ||
                                         (i.Currency.Name == item.Currency))
                                    select i).FirstOrDefault<Partnership>();
                                if (((entity == null) && (arm != null)) && (currency != null))
                                {
                                    entity = PartnershipFactory.BuildNew();
                                    entity.Amount = item.Amount;
                                    entity.Currency = currency;
                                    entity.Month = (int) Enum.Parse(typeof (MonthEnums), item.Month);
                                    entity.Partner = partner;
                                    entity.PartnershipArm = arm;
                                    entity.Year = item.year;
                                    _partnershipService.Create(entity);

                                    num++;
                                    if (OnAdded != null)
                                    {
                                        OnAdded(this, new PartnershipLogCreatedEventArgs(entity));
                                    }
                                    list3.Add(item);
                                }
                                else if ((arm == null) || (currency == null))
                                {
                                    num2++;
                                    list2.Add(item);
                                }
                                else
                                {
                                    num3++;
                                    list.Add(item);
                                }
                            }
                            else
                            {
                                num2++;
                                list2.Add(item);
                            }
                        }
                    }
                }
                if (OnListCompleted != null)
                {
                    var model = new PartnershipLogFileCreatedMailModel
                    {
                        DuplicateLogObject = list,
                        ErrorLogObject = list2,
                        SuccessLogObject = list3
                    };
                    var e = new PartnershipLogProcessedEventArgs(model);
                    OnListCompleted(this, e);
                }
                return string.Format(format, num, num3, num2);
            }
            return format;
        }
    }
}