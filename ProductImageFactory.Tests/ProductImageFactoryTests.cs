using System;
using System.Threading.Tasks;
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

    [Fact(Timeout = 1200)]
    public async Task CreateTwice_WithValidUrl_ReturnsValidProductImage()
    {
        // Test has been implemented as async due to https://github.com/xunit/xunit/issues/2222
        await Task.Delay(1);
        var stubUri = new Uri("https://github.com/Jay-Dee/liz");
        var factory = new ProductImageFactory();

        var image1 = factory.Create(stubUri);
        var image2 = factory.Create(stubUri);

        Assert.NotNull(image1);
        Assert.Equal(stubUri.ToString(), image1.ToString());
        Assert.NotNull(image2);
        Assert.Equal(stubUri.ToString(), image2.ToString());
    }
}