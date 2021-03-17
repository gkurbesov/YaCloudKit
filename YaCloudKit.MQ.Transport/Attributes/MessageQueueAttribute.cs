using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MessageQueueAttribute : Attribute
    {
        public string Name { get; set; }

        public MessageQueueAttribute(string name)
        {
            Name = name;
        }
    }
}
