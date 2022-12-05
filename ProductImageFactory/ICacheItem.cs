using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    public interface ICacheItem<out TCacheItem> {
        TCacheItem CacheItem { get; }

        DateTimeOffset CreationDateTimeOffset { get; }

        DateTimeOffset LastAccessedDateTimeOffset { get; set; }
    }
}
