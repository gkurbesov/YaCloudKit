﻿using System;
using System.Collections.Generic;

namespace YaCloudKit.TTS
{
    /// <summary>
    /// Контекста запроса к Yandex Message Queue
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Параметры для выполнения POST запроса
        /// </summary>
        IDictionary<string, string> RequestParameters { get; }

        /// <summary>
        /// Заголовки для HTTP запроса
        /// </summary>
        IDictionary<string, string> Headers { get; }

        /// <summary>
        /// Utc время выполнения запроса
        /// </summary>
        DateTime RequestDateTime { get; }

        /// <summary>
        /// Добавить параметр в словарь
        /// </summary>
        /// <param name="key">Ключ/имя параметра</param>
        /// <param name="value">значение параметра</param>
        /// <returns></returns>
        IRequestContext AddParameter(string key, string value);

        /// <summary>
        /// Добавить заголовок в словарь
        /// </summary>
        /// <param name="key">ключ http заголовка</param>
        /// <param name="value">значение заголовка</param>
        /// <returns></returns>
        IRequestContext AddHeader(string key, string value);

        /// <summary>
        /// Создает содержимое контента для http запроса
        /// </summary>
        /// <returns></returns>
        byte[] GetContent();
    }
}
