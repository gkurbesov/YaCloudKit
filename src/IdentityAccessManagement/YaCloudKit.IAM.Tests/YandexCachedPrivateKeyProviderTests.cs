using YaCloudKit.IAM.Rsa;

namespace YaCloudKit.IAM.Tests;

[TestClass]
public class YandexCachedPrivateKeyProviderTests
{
    [TestMethod]
    public async Task GetPrivateKeyAsync_WhenPrivateKeyIsNotCached_ShouldCallFuncAgain()
    {
        // Arrange
        var count = 0;

        var privateKeyFunc = new Func<CancellationToken, Task<char[]>>(async _ =>
        {
            count++;
            return "privateKey".ToCharArray();
        });

        var privateKeyProvider = new YandexFuncPrivateKeyProvider(privateKeyFunc);

        // Act
        _ = privateKeyProvider.GetPrivateKeyAsync(CancellationToken.None);
        _ = privateKeyProvider.GetPrivateKeyAsync(CancellationToken.None);
        
        // Assert
        Assert.AreEqual(2, count);
    }
    
    [TestMethod]
    public async Task GetPrivateKeyAsync_WhenPrivateKeyIsCached_ShouldNotCallFuncAgain()
    {
        // Arrange
        var count = 0;

        var privateKeyFunc = new Func<CancellationToken, Task<char[]>>(async _ =>
        {
            count++;
            return "privateKey".ToCharArray();
        });

        var privateKeyProvider = new YandexFuncPrivateKeyProvider(privateKeyFunc, true);

        // Act
        _ = privateKeyProvider.GetPrivateKeyAsync(CancellationToken.None);
        _ = privateKeyProvider.GetPrivateKeyAsync(CancellationToken.None);
        
        // Assert
        Assert.AreEqual(1, count);
    }
}