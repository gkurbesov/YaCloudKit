using System;

namespace YaCloudKit.TTS
{
    public class AudioFormat
    {
        public static readonly AudioFormat Ogg = new("oggopus");
        public static readonly AudioFormat Mp3 = new("mp3");
        public static readonly AudioFormat Lpcm8000 = new("lpcm", 8000);
        public static readonly AudioFormat Lpcm16000 = new("lpcm", 16000);
        public static readonly AudioFormat Lpcm48000 = new("lpcm", 48000);

        /// <summary>
        /// Формат синтезируемого аудио.
        /// lpcm  - удиофайл синтезируется в формате LPCM без WAV-заголовка.
        /// oggopus - данные в аудиофайле кодируются с помощью аудиокодека OPUS и упаковываются в контейнер OGG
        /// </summary>
        public string Format { get; }
        /// <summary>
        /// Частота дискретизации синтезируемого аудио.
        /// Применяется для формата lpcm
        /// </summary>
        public int? SampleRateHertz { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">Формат синтезируемого аудио</param>
        public AudioFormat(string format)
            : this(format, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">Формат синтезируемого аудио</param>
        /// <param name="rate">астота дискретизации</param>
        public AudioFormat(string format, int? rate)
        {
            if (string.IsNullOrWhiteSpace(format))
                throw new ArgumentNullException(nameof(format));

            Format = format;

            SampleRateHertz = rate;
        }
    }
}
