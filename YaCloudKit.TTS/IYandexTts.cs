﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.TTS.Model;

namespace YaCloudKit.TTS
{
    public interface IYandexTts
    {
        /// <summary>
        /// Синтезировать речь из обычного текста
        /// </summary>
        /// <param name="text">текст для генерации речи</param>
        /// <param name="voice">параметры голоса</param>
        /// <param name="format">параметры выходного формата</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<YandexTtsResponse> TextToSpeechAsync(string text, VoiceParameters voice, FormatParameters format, CancellationToken cancellationToken = default);
        /// <summary>
        /// Синтезировать речь из текста в формате SSML
        /// </summary>
        /// <param name="text">текст для генерации речи</param>
        /// <param name="voice">параметры голоса</param>
        /// <param name="format">параметры выходного формата</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<YandexTtsResponse> MarkupToSpeechAsync(string text, VoiceParameters voice, FormatParameters format, CancellationToken cancellationToken = default);
    }
}
