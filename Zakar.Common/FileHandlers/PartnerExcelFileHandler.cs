using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Zakar.DataAccess.Service;
using Microsoft.AspNet.Identity;
using Zakar.Models;

namespace Zakar.Common.FileHandlers
{
    public class PartnerExcelFileHandler
    {
        private readonly StagedPartnerService _stagedPartnerService;

        private readonly PartnerService _partnerService;
        private readonly ChurchService _churchService;
        Dictionary<string, int> cellIdMappings = new Dictionary<string, int>();
        Dictionary<string, int> pcfIdMappings = new Dictionary<string, int>();
        private int churchId;

        public PartnerExcelFileHandler(StagedPartnerService stagedPartnerService, PartnerService partnerService, ChurchService churchService)
        {
            _stagedPartnerService = stagedPartnerService;
            _partnerService = partnerService;
            _churchService = churchService;
        }

        public void HandleFile(string fileName, Stream fileStream, int churchId)
        {
            var workBook = new XLWorkbook(fileStream);
            foreach (var i in workBook.Worksheets.Where(i => i.RowsUsed().Any()))
            {
                ProcessWorksheet(i);
            }
        }

        private void ProcessWorksheet(IXLWorksheet workSheet)
        {
            if (workSheet != null)
            {
                foreach (var row in workSheet.RowsUsed())
                {
                    if (row != null && row.RowBelow()  != null)
                    {
                        var currentRow = row.RowBelow();
                        var title = currentRow.FirstCellUsed().GetString();
                        var firstName = currentRow.FirstCellUsed().CellRight(1).GetString();
                        var lastName = currentRow.FirstCellUsed().CellRight(2).GetString();
                        var email = currentRow.FirstCellUsed().CellRight(3).GetString();
                        var phone = currentRow.FirstCellUsed().CellRight(4).GetString();
                        var dateOfBirth = currentRow.FirstCellUsed().CellRight(5).GetDateTime();
                        var pcfUniqueId = currentRow.FirstCellUsed().CellRight(6).GetString();
                        var cellUniqueId = currentRow.FirstCellUsed().CellRight(7).GetString();
                        var pcfId = this.pcfIdMappings.FirstOrDefault(i => i.Key.Equals(pcfUniqueId)).Value;
                        var cellId = this.cellIdMappings.FirstOrDefault(i => i.Key.Equals(cellUniqueId)).Value;
                        var item = new StagedPartner()
                            {
                                ChurchId = churchId,
                                DateCreated = DateTime.Now,
                                Email = email,
                                Phone = phone,
                                DateOfBirth = dateOfBirth,
                                Title = title,
                                FirstName = firstName,
                                LastName = lastName,
                                PCFId = pcfId,
                                CellId = cellId,
                                UniqueId = IDGenerators.UniqueIdGenerator.GenerateUniqueIdForPartner(firstName + lastName),
                            };
                        _stagedPartnerService.Insert(item);
                    }
                }
            }
        }
    }
}