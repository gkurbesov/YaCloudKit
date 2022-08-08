using System;

namespace YaCloudKit.TTS
{
    public class FormatParameters
    {
        public static readonly FormatParameters OGG = new FormatParameters("oggopus");
        public static readonly FormatParameters LPCM8000 = new FormatParameters("lpcm", 8000);
        public static readonly FormatParameters LPCM16000 = new FormatParameters("lpcm", 16000);
        public static readonly FormatParameters LPCM48000 = new FormatParameters("lpcm", 48000);

        /// <summary>
        /// Формат синтезируемого аудио.
        /// lpcm  - удиофайл синтезируется в формате LPCM без WAV-заголовка.
        /// oggopus - данные в аудиофайле кодируются с помощью аудиокодека OPUS и упаковываются в контейнер OGG
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Частота дискретизации синтезируемого аудио.
        /// Применяется для формата lpcm
        /// </summary>
        public int? SampleRateHertz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">Формат синтезируемого аудио</param>
        public FormatParameters(string format)
            : this(format, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">Формат синтезируемого аудио</param>
        /// <param name="rate">астота дискретизации</param>
        public FormatParameters(string format, int? rate)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException(nameof(format));

            Format = format;

            SampleRateHertz = rate;
        }
    }
}
