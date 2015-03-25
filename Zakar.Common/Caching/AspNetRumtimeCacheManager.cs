using System;
using System.Runtime.Caching;

namespace Zakar.Common.Caching
{
    public class AspNetRumtimeCacheManager : ICacheManager
    {
        public object Get(string key)
        {
            return this.Cache[key];
        }

        public void InvalidateCache(string key)
        {
            this.Cache.Remove(key, null);
        }

        public bool IsSet(string key)
        {
            return ((this.Cache != null) && (this.Cache[key] != null));
        }

        public void SetValue(string key, object value, int minutes)
        {
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes((double)minutes)
            };
            this.Cache.Add(new CacheItem(key, value), policy);
        }

        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
    }
}