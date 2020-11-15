using System;
using System.Collections.Generic;
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

            return default;
        }

        protected void ThrowIfDisposed()
        {

        }
    }
}
