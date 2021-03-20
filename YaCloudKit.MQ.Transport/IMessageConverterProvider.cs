using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverterProvider
    {
        void Register(string tag, IMessageConverter converter);
        IMessageConverter GetConverter(string tag);
    }
}
