using System.IO;
using System.Net;

namespace YaCloudKit.TTS
{
    public class YandexTtsResponse
    {
        /// <summary>
        /// Статускод ответа
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// GUID запроса, присваивается при включении логирования со стороны Yandex SpeechKit
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Результат синтеза речи
        /// </summary>
        public Stream Content { get; set; }
    }
}
