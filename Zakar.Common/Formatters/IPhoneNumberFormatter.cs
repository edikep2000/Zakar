using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zakar.Common.Formatters
{
    public interface IPhoneNumberFormatter
    {
        string FormatPhoneNumber(string phoneNumber);
    }
}
