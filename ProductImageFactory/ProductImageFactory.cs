using System;
using System.Collections.Generic;
using System.Threading;

namespace ProductImageFactory
{
    public class ProductImageFactory : IProductImageFactory
    {
        private readonly ICacheHelper<IProductImage> _cachedProductImages;

        public ProductImageFactory()
            :this(new CacheHelper<IProductImage>())
        {
        }

        public ProductImageFactory(ICacheHelper<IProductImage> cacheHelper)
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
