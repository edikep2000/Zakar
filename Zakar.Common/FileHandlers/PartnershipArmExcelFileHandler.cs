using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Zakar.Common.Events.EventConsumers;
using Zakar.DataAccess.Service;
using Zakar.DataAccess.Utils;
using Zakar.Models;
using Zakar.ViewModels;
using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.FileHandlers
{
    //public class PartnershipArmExcelFileHandler : IFileHandler
    //{
    //    public delegate void PartnerhipArmFileProcessedEventHandler(object s, PartnershipArmUploadedEventArgs e);

    //    public delegate void PartnershipArmListProcessedEventHandler(object s, PartnershipArmListProcessedEventArgs e);

    //    private readonly EventConsumers _consumer;
    //    private readonly PartnershipArmService _partnershipArmRepository;
       
    //    public PartnershipArmExcelFileHandler(EventConsumers consumer, PartnershipArmService partnershipArmRepository)
    //    {
    //        _consumer = consumer;
    //        _partnershipArmRepository = partnershipArmRepository;
    //        _consumer.RegisterHandlerWhenPartnershipArmFileIsUploaded(this);
    //        _consumer.RegisterHandlerWhenPartnershipArmListIsProcessed(this);
    //    }



    //    public string HandleFile(string fileName, Stream fileStream)
    //    {
    //        IList<PartnershipArmUploadedObject> uploadedObject = new List<PartnershipArmUploadedObject>();
    //        int num = 0;
    //        int num2 = 0;
    //        var workbook = new XLWorkbook(fileStream);
    //        IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
    //        if (worksheet != null)
    //        {
    //            IXLRow row = worksheet.FirstRowUsed();
    //            worksheet.FirstColumnUsed();
    //            worksheet.LastColumnUsed();
    //            IXLRow row2 = row.RowBelow();
    //            using (List<IXLRow>.Enumerator enumerator = worksheet.RowsUsed().ToList().GetEnumerator())
    //            {
    //                while (enumerator.MoveNext())
    //                {
    //                    if (row2 != null)
    //                    {
    //                        var item = new PartnershipArmUploadedObject();
    //                        if (row2.CellsUsed().Count() >= 3)
    //                        {
    //                            item.Name = row2.Cell(1).GetString();
    //                            item.Description = row2.Cell(2).GetString();
    //                            item.ShortForm = row2.Cell(3).GetString();
    //                            uploadedObject.Add(item);
    //                            num++;
    //                        }
    //                        else
    //                        {
    //                            if (row2.CellsUsed().Any())
    //                            {
    //                                row2.Style.Fill.SetBackgroundColor(XLColor.Red);
    //                            }
    //                            num2++;
    //                        }
    //                    }
    //                    if (row2 != null)
    //                    {
    //                        row2 = row2.RowBelow();
    //                    }
    //                }
    //            }
    //            using (var stream = new MemoryStream())
    //            {
    //                workbook.SaveAs(stream);
    //                stream.Position = 0L;
    //                if (OnFileProcessed != null)
    //                {
    //                    OnFileProcessed(this, new PartnershipArmUploadedEventArgs(stream));
    //                }
    //            }
    //        }
    //        return ProcessContent(uploadedObject);
    //    }

    //    public event PartnerhipArmFileProcessedEventHandler OnFileProcessed;

    //    public event PartnershipArmListProcessedEventHandler OnListProcessed;

    //    private string ProcessContent(IList<PartnershipArmUploadedObject> uploadedObject)
    //    {
    //        string format = "{0} results added successfully, {1} duplicate records detected";
    //        IList<PartnershipArmUploadedObject> list = new List<PartnershipArmUploadedObject>();
    //        IList<PartnershipArmUploadedObject> list2 = new List<PartnershipArmUploadedObject>();
    //        if ((uploadedObject != null) && uploadedObject.Any())
    //        {
    //            IQueryable<PartnershipArm> all = _partnershipArmRepository.GetAll();
    //            int num = 0;
    //            int num2 = 0;
    //            using (IEnumerator<PartnershipArmUploadedObject> enumerator = uploadedObject.GetEnumerator())
    //            {
    //                while (enumerator.MoveNext())
    //                {
    //                    PartnershipArmUploadedObject item = enumerator.Current;
    //                    if (
    //                        all.FirstOrDefault(
    //                            i => i.ShortFormName.Equals(item.ShortForm, StringComparison.CurrentCultureIgnoreCase)) ==
    //                        null)
    //                    {
    //                        PartnershipArm entity = PartnershipArmFactory.BuildNew();
    //                        entity.Name = item.Name;
    //                        entity.Description = item.Description;
    //                        entity.ShortFormName = item.ShortForm;
    //                        _partnershipArmRepository.Create(entity);
    //                        list.Add(item);
    //                        num++;
    //                    }
    //                    else
    //                    {
    //                        num2++;
    //                        list2.Add(item);
    //                    }
    //                }
    //            }
    //            format = string.Format(format, num, num2);
    //            var model = new PartnershipArmFileCreatedMailModel
    //            {
    //                DuplicateLogObject = list2,
    //                SuccessLogObject = list
    //            };
    //            var e = new PartnershipArmListProcessedEventArgs(model);
    //            if (OnListProcessed != null)
    //            {
    //                OnListProcessed(this, e);
    //            }
    //        }
    //        return format;
    //    }
    //}
}