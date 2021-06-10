﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport
{
    /// <summary>
    /// Провайдер типов сообщений
    /// </summary>
    public interface IMessageTypeProvider
    {
        /// <summary>
        /// Зарегистрировать провайдер типов сообщений
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tag"></param>
        IMessageTypeProvider Register<T>(string tag);
        /// <summary>
        /// Зарегистрировать провайдер типов сообщений
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="type"></param>
        IMessageTypeProvider Register(string tag, Type type);
        /// <summary>
        /// Получить тип сообщения
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        Type GetMessageType(string tag);
        /// <summary>
        /// Ищит тег для зарегистрированного типа сообщения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetMessageTag<T>();
        /// <summary>
        /// Ищит тег для зарегистрированного типа сообщения
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetMessageTag(Type type);
    }
}
