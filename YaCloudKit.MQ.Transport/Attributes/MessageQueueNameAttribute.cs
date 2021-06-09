using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MessageQueueNameAttribute : Attribute, IMessagePropertyAttribute
    {
        public string Name { get; set; }

        public MessageQueueNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
