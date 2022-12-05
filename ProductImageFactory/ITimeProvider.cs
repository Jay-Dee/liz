using System;

namespace ProductImageFactory {
    public interface ITimeProvider
    {
        DateTimeOffset GetCurrentDateTime();
    }
}
