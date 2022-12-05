using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    internal class CacheHelper<TCacheObject> 
        : ICacheHelper<TCacheObject>
    {
        private readonly IDictionary<string, ICacheItem<TCacheObject>> _internalCache;

        public CacheHelper()
        {
            _internalCache = new Dictionary<string, ICacheItem<TCacheObject>>();
        }

        public bool ContainsKey(string key)
        {
            return _internalCache.ContainsKey(key);
        }

        public void Add(string key, TCacheObject value)
        {
            var cacheItem = new CachedItem<TCacheObject>(value, DateTimeOffset.UtcNow);
            _internalCache.Add(key, cacheItem);
        }

        public TCacheObject this[string key]
        {
            get
            {
                var cachedItem = _internalCache[key];
                cachedItem.LastAccessedDateTimeOffset = DateTimeOffset.UtcNow;
                return cachedItem.CacheItem;
            }
        }
    }
}
