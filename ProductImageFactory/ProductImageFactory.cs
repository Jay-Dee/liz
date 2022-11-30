using System;
using System.Threading;

namespace ProductImageFactory
{
    public interface IProductImageFactory
    {
        ProductImage Create(Uri uri);
    }

    internal class ProductImageFactory : IProductImageFactory
    {
        public ProductImage Create(Uri uri)
        {
            //Getting image over a slow link takes ages..
            Thread.Sleep(1000);

            return new ProductImage(uri);
        }
    }
}
