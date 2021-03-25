using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    public interface IMessagePropertyAttribute
    {
        string Name { get; }
    }
}
