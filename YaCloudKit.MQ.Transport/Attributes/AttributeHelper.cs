using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    public class AttributeHelper
    {
        public static string GetMessageName(Type value)
        {
            var attributes = value.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr is MessageQueueAttribute messageAttribute)
                {
                    return messageAttribute.Name;
                }
            }
            return value.GetType().Name;
        }

        public static string GetMessageName(object value) =>
            GetMessageName(value.GetType());
    }
}
