using System;
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
            IProductImage createdProductImage;

            try
            {
                if (_cacheHelper.ContainsKey(uri.ToString()))
                {
                    createdProductImage = _cacheHelper[uri.ToString()];
                }
                else
                {
                    createdProductImage = CreateAndAddToCache(uri);
                }
                
            }
            catch (Exception ex)
            {
                if (ex is CacheItemExpiredException or CacheItemNotFoundException)
                {
                    createdProductImage = CreateAndAddToCache(uri);
                }
                else
                {
                    throw;
                }
            }

            return createdProductImage;
        }

        private IProductImage CreateAndAddToCache(Uri uri)
        {
            //Getting image over a slow link takes ages..
            Thread.Sleep(1000);
            IProductImage createdProductImage = new ProductImage(uri);
            _cacheHelper.Add(uri.ToString(), createdProductImage);

            return createdProductImage;
        }
    }
}
