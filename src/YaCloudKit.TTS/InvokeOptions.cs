using YaCloudKit.TTS.Model;

namespace YaCloudKit.TTS
{
    /// <summary>
    /// Опции выполнения запроса
    /// </summary>
    public class InvokeOptions
    {
        /// <summary>
        /// Текст для синтеза речи
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Флаг использования SSML
        /// </summary>
        public bool SSML { get; set; }
        /// <summary>
        /// Параметры голоса для синтеза
        /// </summary>
        public VoiceParameters Voice { get; set; }
        /// <summary>
        /// ПЕараметры выходных данных ауди-файла
        /// </summary>
        public FormatParameters AudioFormat { get; set; }
    }
}
