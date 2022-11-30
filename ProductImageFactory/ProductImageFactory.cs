using System;
using System.Collections.Generic;
using System.Threading;

namespace ProductImageFactory
{
    public class ProductImageFactory : IProductImageFactory
    {
        private readonly IDictionary<string, IProductImage> _cachedProductImages;

        public ProductImageFactory()
            :this(new Dictionary<string, IProductImage>())
        {
        }

        public ProductImageFactory(IDictionary<string, IProductImage> cacheHelper)
        {
            _cachedProductImages = cacheHelper;
        }

        public IProductImage Create(Uri uri)
        {
            if (!_cachedProductImages.ContainsKey(uri.ToString()))
            {
                //Getting image over a slow link takes ages..
                Thread.Sleep(1000);
                _cachedProductImages.Add(uri.ToString(), new ProductImage(uri));
            }
            return _cachedProductImages[uri.ToString()];
        }
    }
}
