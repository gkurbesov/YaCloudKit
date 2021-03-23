using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    public class AttributeHelper
    {
        public static string GetMessageName(object value)
        {
            var attributes = value.GetType().GetCustomAttributes(true);
            foreach(var attr in attributes)
            {
                if(attr is MessageQueueAttribute messageAttribute)
                {
                    return messageAttribute.Name;
                }
            }
            return value.GetType().Name;
        }
    }
}
