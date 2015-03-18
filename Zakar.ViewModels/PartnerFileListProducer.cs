using System;
using System.Collections.Generic;
using System.Linq;

namespace Zakar.ViewModels
{
    public class PartnerFileListProducer
    {
        public static List<string> ColumnHeadings
        {
            get
            {
                return Enum.GetNames(typeof(PartnerFileEnum)).ToList<string>();
            }
        }
    }
}