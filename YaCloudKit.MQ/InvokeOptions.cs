using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Marshallers;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Requests;

namespace YaCloudKit.MQ
{
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
        public IUnmarshallerr ResponseUnmarshaller { get; set; }
    }
}
