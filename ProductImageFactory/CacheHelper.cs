using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    public class CacheHelper<TCacheObject> 
        : ICacheHelper<TCacheObject> where TCacheObject : class
    {
        private const int CacheLifeTimeInMinutes = 10;

        private readonly ITimeProvider _timeProvider;
        private readonly IDictionary<string, ICacheItem<TCacheObject>> _internalCache;

        public CacheHelper() : this(new TimeProvider())
        {
        }

        public CacheHelper(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _internalCache = new Dictionary<string, ICacheItem<TCacheObject>>();
        }

        public bool ContainsKey(string key)
        {
            return _internalCache.ContainsKey(key);
        }

        public void Add(string key, TCacheObject value)
        {
            var cacheItem = new CachedItem<TCacheObject>(value, _timeProvider.GetCurrentDateTime());
            _internalCache.Add(key, cacheItem);
        }

        public TCacheObject this[string key] => ExtractExistingUnExpiredObject(key);

        private TCacheObject ExtractExistingUnExpiredObject(string key)
        {

            if (ContainsKey(key))
            {
                var cachedItem = _internalCache[key];
                var cacheItemExpiry = cachedItem.CreationDateTimeOffset.AddMinutes(CacheLifeTimeInMinutes);
                return cacheItemExpiry > _timeProvider.GetCurrentDateTime() 
                    ? cachedItem.CacheItem 
                    : throw new CacheItemExpiredException($"Item with Key = {key} has expired at {cacheItemExpiry:dd-MMM-yyyy hh:mm:sss:ffff}");
            }

            throw new CacheItemNotFoundException($"Item not found for Key = {key}");
        }
    }

    public class CacheItemNotFoundException : Exception
    {
        public CacheItemNotFoundException(string exceptionMessage) : base(exceptionMessage)
        {
            
        }
    }

    public class CacheItemExpiredException : Exception
    {
        public CacheItemExpiredException(string exceptionMessage) : base(exceptionMessage) 
        {
            
        }
    }
}
