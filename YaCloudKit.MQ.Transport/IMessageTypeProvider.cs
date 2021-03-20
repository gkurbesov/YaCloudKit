using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageTypeProvider
    {
        void Register<T>(string tag);
        void Register(string tag, Type type);
        Type GetMessageType(string tag);
    }
}
