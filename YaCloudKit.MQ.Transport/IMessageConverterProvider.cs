using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverterProvider
    {
        IEnumerable<IMessageConverter> Values { get; }
        /// <summary>
        /// Зарегистрировать конмертер сообщений
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="converter"></param>
        IMessageConverterProvider Register(string tag, IMessageConverter converter);
        /// <summary>
        /// Получить конвертер
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        IMessageConverter GetConverter(string tag);
        /// <summary>
        /// Получить тэг конвертера
        /// </summary>
        /// <param name="converter"></param>
        /// <returns></returns>
        string GetTag(IMessageConverter converter);
        /// <summary>
        /// Получить конвертер по умолчанию или Null
        /// </summary>
        /// <returns></returns>
        IMessageConverter GetDefault();
    }
}
