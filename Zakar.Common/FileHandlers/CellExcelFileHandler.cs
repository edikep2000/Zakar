using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Zakar.Common.IDGenerators;
using Zakar.DataAccess.Service;
using Zakar.Models;

namespace Zakar.Common.FileHandlers
{
    public class CellExcelFileHandler
    {
        private readonly StagedCellService _cellService;
        private readonly PCFService _pcfService;
        private int _churchId;
        private Dictionary<string, int> _uniqueIdMapping = new Dictionary<string,int>();

        public CellExcelFileHandler(StagedCellService cellService, PCFService pcfService)
        {
            _cellService = cellService;
            _pcfService = pcfService;
        }

        public void HandleFile(string fileName, Stream fileStream, int cid)
        {
           this._churchId = cid;
          

            var workBook = new ClosedXML.Excel.XLWorkbook(fileStream);
            foreach (var i in workBook.Worksheets)
            {
                if (i.RowsUsed().Any())
                {
                    ProcessWorksheet(i);
                }

            }
        }

        private void ProcessWorksheet(IXLWorksheet xlWorksheet)
        {
            if (!_uniqueIdMapping.Any())
                _uniqueIdMapping =
                    _pcfService.GetForChurch(this._churchId)
                               .Select(i => new { i.UniqueId, i.Id })
                               .ToDictionary(t => t.UniqueId, t => t.Id);
            var rows = xlWorksheet.RowsUsed();
            foreach (var row in rows)
            {
                var pcfUniqueId = row.FirstCellUsed().GetString();
                var pcfId =
                    _uniqueIdMapping.FirstOrDefault(
                        i => i.Key.Equals(pcfUniqueId, StringComparison.InvariantCultureIgnoreCase)).Value;

                if (pcfId != 0)
                {
                    var m = new StagedCells()
                    {
                        Name = row.FirstCellUsed().CellRight().GetString(),
                        UniqueId = UniqueIdGenerator.GenerateUniqueIdForPCF(name: row.FirstCellUsed().GetString()),
                        PCFId = pcfId,
                        ChurchId = this._churchId
                    };
                    _cellService.Insert(m);
                }
            }
        }
    }
}