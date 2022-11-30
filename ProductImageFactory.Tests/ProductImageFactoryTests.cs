using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace ProductImageFactory.Tests;

public class ProductImageFactoryTests
{
    [Fact]
    public void Create_WithValidUrl_ReturnsValidProductImage()
    {
        var stubUri = new Uri("https://github.com/Jay-Dee/liz");
        var factory = new ProductImageFactory();

        var image = factory.Create(stubUri);

        Assert.NotNull(image);
        Assert.Equal(stubUri.ToString(), image.ToString());
    }

    [Fact]
    public void CreateTwice_WithValidUrl_ReturnsValidProductImage()
    {
        var stubUri = new Uri("https://github.com/Jay-Dee/liz");
        var mqProduct = new Mock<IProductImage>();
        mqProduct.Setup(_ => _.ToString()).Returns(stubUri.ToString);
        var outProductImage = mqProduct.Object;
        var mqCacheHelper = new Mock<IDictionary<string, IProductImage>>(MockBehavior.Strict);
        mqCacheHelper.Setup(_ => _.ContainsKey(stubUri.ToString())).Returns(true);
        mqCacheHelper.Setup(_ => _.TryGetValue(stubUri.ToString(), out outProductImage)).Returns(true);
        mqCacheHelper.SetupGet(_ => _[stubUri.ToString()]).Returns(outProductImage);

        var factory = new ProductImageFactory(mqCacheHelper.Object);

        var image1 = factory.Create(stubUri);
        var image2 = factory.Create(stubUri);

        Assert.NotNull(image1);
        Assert.Equal(mqProduct.Object, image1);
        Assert.Equal(stubUri.ToString(), image1.ToString());
        Assert.NotNull(image2);
        Assert.Equal(mqProduct.Object, image2);
        Assert.Equal(stubUri.ToString(), image2.ToString());
        mqCacheHelper.Verify(_ => _.ContainsKey(stubUri.ToString()), Times.Exactly(2));
        mqCacheHelper.Verify(_ => _[stubUri.ToString()], Times.Exactly(2));
    }
}