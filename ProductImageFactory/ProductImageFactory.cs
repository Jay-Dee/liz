using System;
using System.Collections.Generic;
using System.Threading;

namespace ProductImageFactory
{
    public class ProductImageFactory : IProductImageFactory
    {
        private readonly ICacheHelper<IProductImage> _cacheHelper;

        public ProductImageFactory()
            : this(new CacheHelper<IProductImage>()) {
        }

        public ProductImageFactory(ICacheHelper<IProductImage> cacheHelper) 
        {
            _cacheHelper = cacheHelper;
        }

        public IProductImage Create(Uri uri)
        {
            if (!_cacheHelper.ContainsKey(uri.ToString()))
            {
                //Getting image over a slow link takes ages..
                Thread.Sleep(1000);
                _cacheHelper.Add(uri.ToString(), new ProductImage(uri));
            }
            return _cacheHelper[uri.ToString()];
        }
    }
}
