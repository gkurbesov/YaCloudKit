using YaCloudKit.MQ.Model.Responses;
using YaCloudKit.Core;

namespace YaCloudKit.MQ.Marshallers
{
    /// <summary>
    /// Интерфейс для демаршаллеров, которые демаршалируют объекты из данных ответа.
    /// </summary>
    public interface IUnmarshaller
    {
        /// <summary>
        /// Демаршаллизация ответа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        T Unmarshall<T>(IResponseContext context) where T : YandexMessageQueueResponse, new();
        /// <summary>
        /// Демаршаллизация ошибки
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        YandexMqServiceException UnmarshallException(IResponseContext context);
    }
}
