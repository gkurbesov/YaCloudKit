using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    public class AttributeHelper
    {
        public static string GetPropertyName<T>(Type value, bool defaultValue = false) where T: IMessagePropertyAttribute
        {
            var attributes = value.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr is T messageAttribute)
                {
                    return messageAttribute.Name;
                }
            }
            return defaultValue ? value.Name : null;
        }

        public static string GetPropertyName<T>(object value, bool defaultValue = false) where T : IMessagePropertyAttribute =>
            GetPropertyName<T>(value.GetType(), defaultValue);
    }
}
