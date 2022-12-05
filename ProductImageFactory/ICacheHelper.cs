using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductImageFactory {
    public interface ICacheHelper<TCacheObject> {
        bool ContainsKey(string key);

        void Add(string key, TCacheObject value);

        TCacheObject this[string key] { get; }
    }
}
