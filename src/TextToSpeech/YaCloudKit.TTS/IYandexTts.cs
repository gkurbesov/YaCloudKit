using System.Threading;
using System.Threading.Tasks;


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
        Task<YandexTtsResponse> TextToSpeechAsync(string text, VoiceParameters voice, AudioFormat format,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Синтезировать речь из текста в формате SSML
        /// </summary>
        /// <param name="text">текст для генерации речи</param>
        /// <param name="voice">параметры голоса</param>
        /// <param name="format">параметры выходного формата</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<YandexTtsResponse> MarkupToSpeechAsync(string text, VoiceParameters voice, AudioFormat format,
            CancellationToken cancellationToken = default);
    }
}