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
        Dictionary<string, int> _cellIdMappings = new Dictionary<string, int>();
        Dictionary<string, int> _pcfIdMappings = new Dictionary<string, int>();
        private int _churchId;
        private readonly CellService _cellService;
        private readonly PCFService _pcfService;

        public PartnerExcelFileHandler(StagedPartnerService stagedPartnerService, PartnerService partnerService, ChurchService churchService, CellService cellService, PCFService pcfService)
        {
            _stagedPartnerService = stagedPartnerService;
            _partnerService = partnerService;
            _churchService = churchService;
            _cellService = cellService;
            _pcfService = pcfService;
        }

        public void HandleFile(string fileName, Stream fileStream, int churchId)
        {
            var workBook = new XLWorkbook(fileStream);
            _churchId = churchId;
           _cellIdMappings = _cellService.GetCellInChurch(churchId).ToDictionary(c => c.UniqueId, k => k.Id);
           _pcfIdMappings = _pcfService.GetForChurch(churchId).ToDictionary(c => c.UniqueId, k => k.Id);
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
                        if (currentRow != null && currentRow.CellsUsed().Any())
                        {
                            var title = currentRow.FirstCellUsed().GetString();
                            var firstName = currentRow.FirstCellUsed().CellRight(1).GetString();
                            var lastName = currentRow.FirstCellUsed().CellRight(2).GetString();
                            var email = currentRow.FirstCellUsed().CellRight(3).GetString();
                            var phone = currentRow.FirstCellUsed().CellRight(4).GetString();
                            var gender = currentRow.FirstCellUsed().CellRight(5).GetString();
                            var dateOfBirth = currentRow.FirstCellUsed().CellRight(6).GetDateTime();
                            var pcfUniqueId = currentRow.FirstCellUsed().CellRight(7).GetString();
                            var cellUniqueId = currentRow.FirstCellUsed().CellRight(8).GetString();
                            var pcfId = this._pcfIdMappings.ContainsKey(pcfUniqueId) ? this._pcfIdMappings.FirstOrDefault(i => i.Key.Equals(pcfUniqueId)).Value : 0;
                            var cellId = this._cellIdMappings.ContainsKey(cellUniqueId) ? this._cellIdMappings.FirstOrDefault(i => i.Key.Equals(cellUniqueId)).Value : 0;
                            var item = new StagedPartner
                            {
                                ChurchId = this._churchId,
                                DateCreated = DateTime.Now,
                                Email = email,
                                Phone = phone,
                                DateOfBirth = dateOfBirth,
                                Title = title,
                                FirstName = firstName,
                                LastName = lastName,
                                PCFId = pcfId,
                                CellId = cellId,
                                Gender = gender,
                                UniqueId = IDGenerators.UniqueIdGenerator.GenerateUniqueIdForPartner(firstName + lastName),
                            };
                            _stagedPartnerService.Insert(item);
                        }
                       
                    }
                }
            }
        }
    }
}