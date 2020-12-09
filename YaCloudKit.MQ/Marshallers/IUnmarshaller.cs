using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    public interface IUnmarshaller
    {
        T Unmarshall<T>(IResponseContext context) where T: YandexMessageQueueResponse, new();
        YandexMqServiceException UnmarshallException(IResponseContext context);
    }
}
