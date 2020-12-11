using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Utils
{
    public static class RequestAttributesBuilder
    {
        public static void NamedAttributes(IRequestContext context, Dictionary<string, string> values)
        {
            var number = 1;
            foreach(var item in values)
            {
                context.AddParametr($"Attribute.{number}.Name", item.Key);
                context.AddParametr($"Attribute.{number}.Value", item.Value);
                number++;
            }
        }
    }
}
