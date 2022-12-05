using System;

namespace ProductImageFactory {
    internal class TimeProvider  : ITimeProvider{
        public DateTimeOffset GetCurrentDateTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
