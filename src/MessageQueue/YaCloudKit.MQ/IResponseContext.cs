using System.IO;
using System.Net;
using System.Net.Http.Headers;

namespace YaCloudKit.MQ
{
    /// <summary>
    /// Контекст с данными ответа от Yandex Message Queue
    /// </summary>
    public interface IResponseContext
    {
        /// <summary>
        /// Статус код ответа HTTP
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Заголовки HTTP ответа
        /// </summary>
        HttpResponseHeaders Headers { get; }

        /// <summary>
        /// Stream содержащий данные тела ответа
        /// </summary>
        Stream ContentStream { get; }
    }
}
