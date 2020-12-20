using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YaCloudKit.TTS.Model;

namespace YaCloudKit.TTS
{
    public interface IYandexTts
    {
        Task<YandexTtsResponse> TextToSpeech(string text, VoiceParameters voice, FormatParameters format);
        Task<YandexTtsResponse> MarkupToSpeech(string text, VoiceParameters voice, FormatParameters format);
    }
}
