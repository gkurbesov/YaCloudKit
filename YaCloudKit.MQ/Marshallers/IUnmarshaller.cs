using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YaCloudKit.MQ.Marshallers
{
    public interface IUnmarshallerr
    {
        Task<T> UnmarshallAsync<T>(HttpResponseMessage response);
    }
}
