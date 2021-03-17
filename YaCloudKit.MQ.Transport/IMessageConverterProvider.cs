using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverterProvider
    {
        void Registration(string tag, IMessageConverter converter);
        IMessageConverter GetConverter(string tag);
    }
}
