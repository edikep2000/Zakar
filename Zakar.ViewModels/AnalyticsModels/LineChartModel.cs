using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.ViewModels.AnalyticsModels
{
    public class LineChartModel
    {
        public LineChartModel()
        {
            this.Month = 0;
            this.Year = DateTime.Now.Year;
        }

        public decimal Amount { get; set; }

        public int Month { get; set; }

        public string Period
        {
            get
            {
                if (this.Month == 0)
                {
                    return this.Year.ToString(CultureInfo.InvariantCulture);
                }
                DateTime time = new DateTime(this.Year, this.Month, 1);
                return time.ToString("MMM - yyyy");
            }
        }

        public int Year { get; set; }
    }
}
