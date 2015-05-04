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
    public class GroupExcelFileHandler
    {

        private readonly StagedGroupService _groupService;
        private readonly ZoneService _zoneService;

        private Dictionary<string, int> _uniqueIdIdMapping = new Dictionary<string, int>();
        public GroupExcelFileHandler(StagedGroupService groupService, ZoneService zoneService)
        {
            _groupService = groupService;
            _zoneService = zoneService;
        }

        public void HandleFile(string fileName, Stream fileStream)
        {
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
            if(!_uniqueIdIdMapping.Any() )
                _uniqueIdIdMapping = _zoneService.GetAll().Select(i => new {i.UniqueId, i.Id}).ToDictionary(t => t.UniqueId, t => t.Id);
            var rows = xlWorksheet.RowsUsed();
            foreach (var row in rows)
            {
                var parentUniqueId = row.FirstCellUsed().GetString();
                var parentId =
                    _uniqueIdIdMapping.FirstOrDefault(i => i.Key.Equals(parentUniqueId, StringComparison.InvariantCultureIgnoreCase)).Value;
                var name = row.FirstCellUsed().CellRight().GetString();
                var m = new StagedGroup()
                    {
                        ZoneId = parentId,
                        Name = name,
                        UniqueId = UniqueIdGenerator.GenerateUniqueIdForGroup(name)
                    };
                _groupService.Insert(m);
            }
        }
    }
}