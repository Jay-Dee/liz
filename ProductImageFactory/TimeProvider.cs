using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    internal class TimeProvider  : ITimeProvider{
        public DateTimeOffset GetCurrentDateTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
