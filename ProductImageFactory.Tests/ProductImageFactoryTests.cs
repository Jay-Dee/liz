using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace ProductImageFactory.Tests;

public class ProductImageFactoryTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void Create_DifferentImages_WithValidUrl_ReturnsValidProductImage(int numberOfImagesToCreate)
    {
        var mqCacheHelper = new Mock<ICacheHelper<IProductImage>>(MockBehavior.Strict);

        for (int creationCounter = 0; creationCounter < numberOfImagesToCreate; creationCounter++)
        {
            var stubUri = new Uri($"https://github.com/Jay-Dee/liz/{creationCounter}");
            var mqProduct = new Mock<IProductImage>();
            mqProduct.Setup(_ => _.ToString()).Returns(stubUri.ToString);
            mqCacheHelper.Setup(_ => _.ContainsKey(stubUri.ToString())).Returns(true);
            mqCacheHelper.SetupGet(_ => _[stubUri.ToString()]).Returns(mqProduct.Object);
        }
        
        var factory = new ProductImageFactory(mqCacheHelper.Object);

        for (int creationCounter = 0; creationCounter < numberOfImagesToCreate; creationCounter++)
        {
            var stubUri = new Uri($"https://github.com/Jay-Dee/liz/{creationCounter}");

            var image = factory.Create(stubUri);

            Assert.NotNull(image);
            Assert.Equal(stubUri.ToString(), image.ToString());

            mqCacheHelper.Verify(_ => _.ContainsKey(stubUri.ToString()), Times.Once);
            mqCacheHelper.Verify(_ => _[stubUri.ToString()], Times.Once);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    public void Create_SameImage_WithValidUrl_ReturnsValidProductImage(int numberOfTimesToCreate)
    {
        var stubUri = new Uri("https://github.com/Jay-Dee/liz");
        var mqProduct = new Mock<IProductImage>();
        mqProduct.Setup(_ => _.ToString()).Returns(stubUri.ToString);
        var mqCacheHelper = new Mock<ICacheHelper<IProductImage>>(MockBehavior.Strict);
        mqCacheHelper.Setup(_ => _.ContainsKey(stubUri.ToString())).Returns(true);
        mqCacheHelper.SetupGet(_ => _[stubUri.ToString()]).Returns(mqProduct.Object);

        var factory = new ProductImageFactory(mqCacheHelper.Object);

        for (int creationCounter = 0; creationCounter < numberOfTimesToCreate; creationCounter++)
        {
            var createdImage = factory.Create(stubUri);

            Assert.NotNull(createdImage);
            Assert.Equal(mqProduct.Object, createdImage); // verifies usage of cached object
            Assert.Equal(stubUri.ToString(), createdImage.ToString());
        }
        mqCacheHelper.Verify(_ => _.ContainsKey(stubUri.ToString()), Times.Exactly(numberOfTimesToCreate));
        mqCacheHelper.Verify(_ => _[stubUri.ToString()], Times.Exactly(numberOfTimesToCreate));
    }
}