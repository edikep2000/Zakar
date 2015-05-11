using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakar.Common.Builders;
using Zakar.Common.Converters;
using Zakar.Common.Events.EventArgs;
using Zakar.Common.FileHandlers;
using Zakar.Common.Messaging;
using Zakar.DataAccess.Service;
using Zakar.DataAccess.Utils;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.Common.Events.EventConsumers
{
    //public class EventConsumers
    //{
      

      
    //    public IMessageBuilder<Partnership, string> MessageBuilder { get; set; }


    //    public IMessageSender MessageSender { get; set; }


    //    public NotificationCategoryService NotificationCategoryService { get; set; }


    //    public QueuedNotificationService NotificationService { get; set; }

    //    public IMessageBuilder<Partner, string> PartnerMessageBuilder { get; set; }

    //    public void OnPartnerFileUploaded(object sender, PartnerFileUploadedEventArgs e)
    //    {
    
    //    }

    //    public void OnPartnerListProcessed(object sender, PartnerListProcessedEventArgs e)
    //    {
          
    //    }

    //    public void OnPartnerRecordCreated(object sender, PartnerCreatedEventArgs e)
    //    {
    //        Partner getPartner = e.GetPartner;
    //        string str = PartnerMessageBuilder.BuildMessage(getPartner);
    //        QueuedNotification notification = QueuedNotificationFactory.BuildNew();
    //        notification.Message = str;
    //        notification.LastTried = DateTime.Now.AddHours(-1.0);
    //        notification.RecipientAddress = getPartner.Phone;
    //        notification.NotificationCategory =
    //            NotificationCategoryService.GetAll()
    //                                       .FirstOrDefault(
    //                                           i => i.Name.Equals("SMS", StringComparison.CurrentCultureIgnoreCase));
    //        NotificationService.Create(notification);
    //    }

    //    public void OnPartnershipArmFileUploaded(object sender, PartnershipArmUploadedEventArgs e)
    //    {
           
    //    }

    //    public void OnPartnershipArmListProcessed(object sender, PartnershipArmListProcessedEventArgs e)
    //    {
           
    //    }

    //    public void OnPartnershipListProcessed(object sender, PartnershipLogProcessedEventArgs e)
    //    {
            
    //    }

    //    public void OnPartnershipLogFileUploaded(object sender, PartnershipLogFileUploadedEventArgs e)
    //    {
           
    //    }

    //    public void OnPartnershipRecordCreated(object sender, PartnershipLogCreatedEventArgs e)
    //    {
    //        Partnership getPartnership = e.GetPartnership;
    //        string str = MessageBuilder.BuildMessage(getPartnership);
    //        QueuedNotification notification = QueuedNotificationFactory.BuildNew();
    //        notification.Message = str;
    //        notification.LastTried = DateTime.Now.AddHours(-1.0);
    //        notification.RecipientAddress = getPartnership.Partner.Phone;
    //        notification.NotificationCategory =
    //            NotificationCategoryService.GetAll()
    //                                       .FirstOrDefault(
    //                                           i => i.Name.Equals("SMS", StringComparison.CurrentCultureIgnoreCase));
    //        SMSMessage message = NotificationToSmsTranslator.BuildMessageFromNotification(notification);
    //        MessageSender.SendMessage(message);
    //    }

    //    public void RegisterHandlerWhenPartnerFileIsProcessed(PartnerUploadExcelFileHandler handler)
    //    {
    //        handler.OnFileProcessed += OnPartnerFileUploaded;
    //    }

    //    public void RegisterHandlerWhenPartnerListIsProcessed(PartnerUploadExcelFileHandler handler)
    //    {
    //        handler.OnListProcessed += OnPartnerListProcessed;
    //    }

    //    public void RegisterHandlerWhenPartnershipArmFileIsUploaded(PartnershipArmExcelFileHandler handler)
    //    {
    //        handler.OnFileProcessed += OnPartnershipArmFileUploaded;
    //    }

    //    public void RegisterHandlerWhenPartnershipArmListIsProcessed(PartnershipArmExcelFileHandler handler)
    //    {
    //        handler.OnListProcessed += OnPartnershipArmListProcessed;
    //    }

    //    public void RegisterHandlerWhenPartnershipLogFileUploaded(PartnershipLogExcelFileHandler handler)
    //    {
    //        handler.OnProcessed += OnPartnershipLogFileUploaded;
    //    }

    //    public void RegisterHandlerWhenPartnershipLogListIsProcessed(PartnershipLogExcelFileHandler handler)
    //    {
    //        handler.OnListCompleted += OnPartnershipListProcessed;
    //    }

    //    public void RegisterHandlerWhenPartnershipLogRecordedIsCreated(PartnershipLogExcelFileHandler handler)
    //    {
    //        handler.OnAdded += OnPartnershipRecordCreated;
    //    }
    //}
}
