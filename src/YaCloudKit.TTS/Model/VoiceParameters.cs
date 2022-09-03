using System;

namespace YaCloudKit.TTS
{
    /// <summary>
    /// Параметры голоса для генерации
    /// </summary>
    public class VoiceParameters
    {
        /// <summary>
        /// Женский русский голос Alena
        /// </summary>
        public static readonly VoiceParameters Alena = new(VoiceName.Alena);

        /// <summary>
        /// Мужской русский голос Filipp
        /// </summary>
        public static readonly VoiceParameters Filipp = new(VoiceName.Filipp);

        /// <summary>
        /// Женский русский голос Jane
        /// </summary>
        public static readonly VoiceParameters Jane = new(VoiceName.Jane);

        /// <summary>
        /// Женский русский голос Omazh
        /// </summary>
        public static readonly VoiceParameters Omazh = new(VoiceName.Omazh);

        /// <summary>
        /// Мужской русский голос Zahar
        /// </summary>
        public static readonly VoiceParameters Zahar = new(VoiceName.Zahar);

        /// <summary>
        /// Мужской русский голос Ermil
        /// </summary>
        public static readonly VoiceParameters Ermil = new(VoiceName.Ermil);


        /// <summary>
        /// Название голоса. Подробнее см. список голосов
        /// </summary>
        public VoiceName Name { get; init; }

        /// <summary>
        /// Основной язык, который поддерживает голос.
        /// На этом языке разговаривал диктор при создании этого голоса.
        /// </summary>
        public VoiceLanguage Language { get; init; }

        /// <summary>
        /// Скорость (темп) синтезированной речи.
        /// Скорость речи задается дробным числом в диапазоне от 0.1 до 3.0
        /// </summary>
        public string Speed { get; init; }

        /// <summary>
        /// Амплуа или эмоциональная окраска голоса. Поддерживается только при выборе русского языка.
        /// </summary>
        public VoiceEmotion Emotion { get; init; }

        /// <summary>
        /// Инициалзация параметров голоса для генерации речи
        /// </summary>
        /// <param name="name">Название голоса</param>
        public VoiceParameters(VoiceName name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}