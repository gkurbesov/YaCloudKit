using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Tests
{
    public class YandexMqSignerTests
    {
        [Fact]
        public void ToHexString_Lowercase_Test()
        {
            var bytes = new byte[] { 0x01, 0xAB, 0xF1 };
            var expectedHexString = "01abf1";

            var result = YandexMqSigner.ToHexString(bytes, lowercase: true);

            Assert.Equal(expectedHexString, result);
        }

        [Fact]
        public void ToHexString_Uppercase_Test()
        {
            var bytes = new byte[] { 0x01, 0xAB, 0xF1 };
            var expectedHexString = "01ABF1";

            var result = YandexMqSigner.ToHexString(bytes, lowercase: false);

            Assert.Equal(expectedHexString, result);
        }

        [Fact]
        public void HmacSHA256_Test()
        {
            var key = Encoding.UTF8.GetBytes("key-string");
            var expectedValue = new byte[] { 22, 220, 59, 206, 215, 34, 239, 140, 181, 227, 71, 235, 231, 191, 162, 85, 79, 238, 252, 104, 96, 22, 215, 1, 240, 196, 204, 67, 45, 107, 106, 45 };
            var result = YandexMqSigner.HmacSHA256("test-value", key);

            Assert.True(expectedValue.SequenceEqual(result));
        }

        [Fact]
        public void GetSignatureKey_Test()
        {
            var expectedValue = new byte[] { 210, 11, 223, 66, 137, 8, 16, 44, 30, 92, 51, 84, 60, 71, 215, 246, 213, 46, 215, 18, 197, 113, 148, 29, 2, 224, 177, 222, 8, 127, 44, 169 };
            var result = YandexMqSigner.GetSignatureKey("test-key", "test-time", "test-region", "test-service");

            Assert.True(expectedValue.SequenceEqual(result));
        }
    }
}
