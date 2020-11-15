using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Marshallers
{
    public interface IMarshaller<T>
    {
        IRequestContext Marshall(T input);
    }
}
