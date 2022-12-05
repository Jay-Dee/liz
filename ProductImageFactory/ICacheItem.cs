using System;

namespace ProductImageFactory {
    public interface ICacheItem<out TCacheItem> {
        TCacheItem CacheItem { get; }

        DateTimeOffset CreationDateTimeOffset { get; }

        DateTimeOffset LastAccessedDateTimeOffset { get; set; }
    }
}
