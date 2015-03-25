using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakar.Common.Caching
{
    public interface ICacheManager
    {
        object Get(string key);
        void InvalidateCache(string key);
        bool IsSet(string key);
        void SetValue(string key, object value, int minutes);
    }
}
