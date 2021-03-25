using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    public class AttributeHelper
    {
        public static string GetPropertyName<T>(Type value) where T: IMessagePropertyAttribute
        {
            var attributes = value.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr is T messageAttribute)
                {
                    return messageAttribute.Name;
                }
            }
            return value.GetType().Name;
        }

        public static string GetPropertyName<T>(object value) where T : IMessagePropertyAttribute =>
            GetPropertyName<T>(value.GetType());
    }
}
