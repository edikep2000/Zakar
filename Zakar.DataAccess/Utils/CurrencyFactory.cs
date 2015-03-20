using Zakar.Models;

namespace Zakar.DataAccess.Utils
{
    public class CurrencyFactory
    {
        public static Currency BuildNewCurrency()
        {
            return new Currency();
        }
    }
}