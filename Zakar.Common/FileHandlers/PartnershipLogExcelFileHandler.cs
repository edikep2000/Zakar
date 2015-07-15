using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Microsoft.AspNet.Identity;
using WebMatrix.WebData;
using Zakar.Common.Events.EventConsumers;
using Zakar.DataAccess.Service;
using Zakar.DataAccess.Utils;
using Zakar.Models;
using Zakar.ViewModels;
using Zakar.ViewModels.EmailModels;

namespace Zakar.Common.FileHandlers
{
    public class PartnershipLogExcelFileHandler
    {
        private readonly ChurchService _churchService;
        private readonly PartnerService _partnerService;
        private readonly PartnershipArmService _partnershipArmService;
        private readonly CurrencyService _currency;
        private int _churchId = 0;

        private readonly StagedPartnershipService _stagedPartnershipService;
        private Dictionary<string, int> _currencySymbol = new Dictionary<string, int>();
        private Dictionary<String, int> _partnerIdMappings = new Dictionary<string, int>();
        private Dictionary<String, int> _partnershipArmMappings = new Dictionary<string, int>();

        public PartnershipLogExcelFileHandler(ChurchService churchService, PartnerService partnerService,
            StagedPartnershipService stagedPartnershipService, PartnershipArmService partnershipArmService,
            CurrencyService currency)
        {
            _churchService = churchService;
            _partnerService = partnerService;
            _stagedPartnershipService = stagedPartnershipService;
            _partnershipArmService = partnershipArmService;
            _currency = currency;
        }

        public void HandleFile(String fileName, Stream fileStream, int churchId)
        {
            _churchId = churchId;
            var workBook = new XLWorkbook(fileStream);
            _partnerIdMappings = _partnerService.Find(i => i.ChurchId == churchId)
                .ToDictionary(t => t.UniqueId, K => K.Id);
            _partnershipArmMappings = _partnershipArmService.GetAll().ToDictionary(K => K.ShortFormName, C => C.Id);
            _currencySymbol = _currency.GetAll().ToDictionary(y => y.Symbol, t => t.Id);
            foreach (var i in workBook.Worksheets.Where(i => i.RowsUsed().Any()))
            {
                ProcessWorkSheet(i);
            }
        }

        private void ProcessWorkSheet(IXLWorksheet worksheet)
        {
            if (worksheet != null)
            {
                foreach (var row in worksheet.RowsUsed())
                {
                    if (row != null && row.RowBelow() != null)
                    {
                        var currentRow = row.RowBelow();
                        if (currentRow != null && currentRow.CellsUsed().Any())
                        {
                           
                            var partnerUniqueId = currentRow.FirstCellUsed().GetString();
                            var armShort = currentRow.FirstCellUsed().CellRight(1).GetString();
                            var amount = currentRow.FirstCellUsed().CellRight(2).GetDouble();
                            var currency = currentRow.FirstCellUsed().CellRight(3).GetString();
                            var month = currentRow.FirstCellUsed().CellRight(4).GetString();
                            var year = currentRow.FirstCellUsed().CellRight(5).GetString();
                            var armId = this._partnershipArmMappings.ContainsKey(armShort)
                                ? this._partnershipArmMappings.FirstOrDefault(i => i.Key.Equals(armShort)).Value
                                : 0;
                            var currencyId = this._currencySymbol.ContainsKey(currency)
                                ? this._currencySymbol.FirstOrDefault(i => i.Key.Equals(currency)).Value
                                : 0;
                            var partnerId = this._partnerIdMappings.ContainsKey(partnerUniqueId)
                                ? this._partnerIdMappings.FirstOrDefault(i => i.Key.Equals(partnerUniqueId)).Value
                                : 0;

                            var item = new StagedPartnership()
                            {
                                Amount = (decimal) amount,
                                ArmId = armId,
                                ChurchId = _churchId,
                                CurrencyId = currencyId,
                                DateCreated = DateTime.Now,
                                Month = 1,
                                PartnerId = partnerId,
                                Year = Convert.ToInt32(year)
                            };
                            _stagedPartnershipService.Insert(item);
                        }
                    }
                }
            }
        }
    }
}