using System;

namespace ProductImageFactory 
{
    internal class CachedItem<TCacheItem> : ICacheItem<TCacheItem>
    {
        public CachedItem(TCacheItem productImage, DateTimeOffset creationInstance)
        {
            CacheItem = productImage;
            LastAccessedDateTimeOffset = CreationDateTimeOffset = creationInstance;
        }

        public TCacheItem CacheItem { get; }
        public DateTimeOffset CreationDateTimeOffset { get; }
        public DateTimeOffset LastAccessedDateTimeOffset { get; set; }
    }
}
