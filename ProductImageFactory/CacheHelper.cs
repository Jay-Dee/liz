using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    internal class CacheHelper<TCacheObject> : ICacheHelper<TCacheObject>
    {
        private readonly IDictionary<string, TCacheObject> _internalCache;

        public CacheHelper()
        {
            _internalCache = new Dictionary<string, TCacheObject>();
        }

        public bool ContainsKey(string key)
        {
            return _internalCache.ContainsKey(key);
        }

        public void Add(string key, TCacheObject value)
        {
            _internalCache.Add(key, value);
        }

        public TCacheObject this[string key] => _internalCache[key];
    }
}
