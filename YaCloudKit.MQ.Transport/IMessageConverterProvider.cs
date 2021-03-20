using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverterProvider
    {
        /// <summary>
        /// Зарегистрировать конмертер сообщений
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="converter"></param>
        void Register(string tag, IMessageConverter converter);
        /// <summary>
        /// Получить конвертер
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        IMessageConverter GetConverter(string tag);
    }
}
