using YaCloudKit.MQ.Marshallers;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ
{
    /// <summary>
    /// Опции выполнения запроса
    /// </summary>
    public class InvokeOptions
    {
        /// <summary>
        /// Оригинальный запрос 
        /// </summary>
        public BaseRequest OriginalRequest { get; set; }
        /// <summary>
        /// Маршаллер для создание контекста запроса
        /// </summary>
        public IMarshaller<BaseRequest> RequestMarshaller { get; set; }
        /// <summary>
        /// Унмаршаллер для получения результатов ответа от сервера
        /// </summary>
        public IUnmarshaller ResponseUnmarshaller { get; set; }
    }
}
