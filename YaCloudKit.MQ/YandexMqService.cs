using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.MQ.Utils;

namespace YaCloudKit.MQ
{
    public abstract class YandexMqService
    {
        public YandexMqConfig Config { get; set; }

        protected YandexMqService(YandexMqConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            Config = config;
        }

        protected async Task<TResponse> InvokeAsync<TResponse>(InvokeOptions options, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();

            IRequestContext requestContext = options.RequestMarshaller.Marshall(options.OriginalRequest);
            YandexMqHeaderBuilder.AddHeaders(requestContext, Config.EndPoint);

            var signature = new YandexMqSigner(Config).Create(requestContext);
            YandexMqHeaderBuilder.AddHeaderAuthorization(requestContext, signature);

            var content = new ByteArrayContent(requestContext.GetContent());
            YandexMqHeaderBuilder.AddHttpHeaders(content.Headers, requestContext.Headers);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Config.EndPoint);
            request.Content = content;
            YandexMqHeaderBuilder.AddHttpHeaders(request.Headers, requestContext.Headers);

            var httpResponse = await GetHttpClient().SendAsync(request, cancellationToken);
            var response = await options.ResponseUnmarshaller.UnmarshallAsync<TResponse>(httpResponse);

            return response;
        }

        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.Timeout = Config.HttpClientTimeout;
            return client;
        }

        protected void ThrowIfDisposed()
        {

        }
    }
}
