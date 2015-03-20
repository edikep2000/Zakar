using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class SettingsFactory
    {
        public static Setting BuildNewBooleanSetting()
        {
            return new Setting { Value = bool.FalseString };
        }

        public static Setting BuildNewIntegerSetting()
        {
            var setting2 = new Setting {Value = 0.ToString()};
            return setting2;
        }

        public static Setting BuildNewSetting()
        {
            return new Setting();
        }

        public static Setting BuildNewStringSetting()
        {
            return new Setting { Value = " " };
        }
    }
}
