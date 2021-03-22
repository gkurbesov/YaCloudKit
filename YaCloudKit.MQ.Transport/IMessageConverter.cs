using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    public interface IMessageConverter
    {
        /// <summary>
        /// Десериализация сообщения в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        T Deserialize<T>(string messageBody) where T : class;
        /// <summary>
        /// Десериализация сообщения в объект
        /// </summary>
        /// <param name="messageBody"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        object Deserialize(string messageBody, Type type);
        /// <summary>
        /// Сериализация объекта в сообщение для передачи
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Serialize(object value);
    }
}
