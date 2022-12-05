using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace ProductImageFactory.Tests {
    public class CacheHelperTests {
        [Fact]
        public void Get_AfterExpiry_ThrowsCacheExpiredException()
        {
            var stubKey = "Key1";
            var creationTime = DateTimeOffset.UtcNow.AddMinutes(-20);
            var mqTimeProvider = new Mock<ITimeProvider>(MockBehavior.Strict);
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(creationTime);
            
            var cacheHelper = new CacheHelper<object>(mqTimeProvider.Object);
            cacheHelper.Add(stubKey, new object());
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(DateTimeOffset.UtcNow);
            
            var expiredException = Assert.Throws<CacheItemExpiredException>(() => cacheHelper[stubKey]);
            Assert.NotNull(expiredException);
            Assert.Equal($"Item with Key = {stubKey} has expired at {creationTime.AddMinutes(10):dd-MMM-yyyy hh:mm:ss:ffff}", expiredException.Message);
        }

        [Fact]
        public void Get_BeforeExpiry_ReturnsValidCacheObject() {
            var stubKey = "Key1";
            var stubCacheItem = new object();
            var creationTime = DateTimeOffset.UtcNow.AddMinutes(-5);
            var mqTimeProvider = new Mock<ITimeProvider>(MockBehavior.Strict);
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(creationTime);

            var cacheHelper = new CacheHelper<object>(mqTimeProvider.Object);
            cacheHelper.Add(stubKey, stubCacheItem);
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(DateTimeOffset.UtcNow);

            var cachedItem = cacheHelper[stubKey];
            Assert.NotNull(cachedItem);
            Assert.Equal(stubCacheItem, cachedItem);
        }

        [Fact]
        public void Get_AtExpiry_ThrowsCacheExpiredException() {
            var stubKey = "Key1";
            var creationTime = DateTimeOffset.UtcNow.AddMinutes(-10);
            var mqTimeProvider = new Mock<ITimeProvider>(MockBehavior.Strict);
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(creationTime);

            var cacheHelper = new CacheHelper<object>(mqTimeProvider.Object);
            cacheHelper.Add(stubKey, new object());
            mqTimeProvider.Setup(_ => _.GetCurrentDateTime()).Returns(DateTimeOffset.UtcNow);

            var expiredException = Assert.Throws<CacheItemExpiredException>(() => cacheHelper[stubKey]);
            Assert.NotNull(expiredException);
            Assert.Equal($"Item with Key = {stubKey} has expired at {creationTime.AddMinutes(10):dd-MMM-yyyy hh:mm:ss:ffff}", expiredException.Message);
        }

        [Fact]
        public void Add_MultipleTimes_ReturnsLatestObject()
        {
            var stubObj1 = new object();
            var stubObj2 = new object();
            var cacheHelper = new CacheHelper<object>();
            cacheHelper.Add("key", stubObj1);

            var objAfterFirstAdd = cacheHelper["key"];
            Assert.Equal(stubObj1, objAfterFirstAdd);

            cacheHelper.Add("key", stubObj2);
            var objAfterSecondAdd = cacheHelper["key"];
            Assert.Equal(stubObj2, objAfterSecondAdd);
        }
    }
}
