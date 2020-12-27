using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.TTS.Model;

namespace YaCloudKit.TTS
{
    public interface IYandexTts
    {
        Task<YandexTtsResponse> TextToSpeechAsync(string text, VoiceParameters voice, FormatParameters format, CancellationToken cancellationToken = default);
        Task<YandexTtsResponse> MarkupToSpeechAsync(string text, VoiceParameters voice, FormatParameters format, CancellationToken cancellationToken = default);
    }
}
