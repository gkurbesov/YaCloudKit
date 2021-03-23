using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Model.Requests;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageTransport
    {
        Task<IEnumerable<object>> ReceiveMessageAsync(ReceiveMessageRequest message, CancellationToken cancellationToken = default);
        Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default);
    }
}
