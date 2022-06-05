using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ.Tests
{
    public class YandexMqHeaderBuilderTests
    {
        [Fact]
        public void AddMainHeaders_Test()
        {
            var context = new RequestContext();
            context.AddParametr("key", "value");
            var endpoint = new Uri("http://localhost:8080");

            var contentLength = context.GetContent().Length;

            YandexMqHeaderBuilder.AddMainHeaders(context, endpoint);

            Assert.Equal(contentLength, int.Parse(context.Headers["Content-Length"]));
            Assert.Equal("application/x-www-form-urlencoded", context.Headers["Content-Type"]);
            Assert.Equal("localhost:8080", context.Headers["Host"]);
        }

        [Fact]
        public void AddAWSDateHeaders_Test()
        {
            var dt = new DateTime(2022,1,1,0,0,0); 
            var context = new RequestContext() { RequestDateTime = dt };

            YandexMqHeaderBuilder.AddAWSDateHeaders(context);

            Assert.Equal("20220101T000000Z", context.Headers["X-Amz-Date"]);
        }

        [Fact]
        public void AddHeaderAuthorization_Test()
        {
            var value = "TestValue";
            var context = new RequestContext();

            YandexMqHeaderBuilder.AddHeaderAuthorization(context, value);

            Assert.Equal(value, context.Headers["Authorization"]);
        }

        [Fact]
        public void AddHttpHeaders_Test()
        {
            var headers = new HttpRequestMessage().Headers;
            var context = new RequestContext();
            context.AddHeader("key", "value");

            YandexMqHeaderBuilder.AddHttpHeaders(context, headers);

            Assert.Equal("value", context.Headers["key"]);
        }
    }
}
