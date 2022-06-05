using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace YaCloudKit.MQ.Tests.Utils
{
    public class YandexMqHeaderBuilderTests
    {
        [Fact]
        public void AddMainHeaders_Test()
        {
            var context = new RequestContext();
            context.AddParametr("key", "value");

            var contentLength = context.GetContent().Length;


            Assert.True(contentLength > 0);
        }
    }
}
