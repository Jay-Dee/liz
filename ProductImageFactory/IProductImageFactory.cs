using System;

namespace ProductImageFactory;

public interface IProductImageFactory
{
    IProductImage Create(Uri uri);
}