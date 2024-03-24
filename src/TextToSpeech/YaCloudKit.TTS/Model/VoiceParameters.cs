using System;

namespace YaCloudKit.TTS
{
    /// <summary>
    /// Параметры голоса для генерации
    /// </summary>
    public class VoiceParameters
    {
        /// <summary>
        /// Женский немецкий голос Lea
        /// </summary>
        public static readonly VoiceParameters Lea = new(VoiceName.Lea);
        
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
        /// Женский казахский голос
        /// </summary>
        public static readonly VoiceParameters Amira = new(VoiceName.Amira);

        /// <summary>
        /// Мужской казахский голос
        /// </summary>
        public static readonly VoiceParameters Madi = new(VoiceName.Madi);

        /// <summary>
        /// Мужской английский голос
        /// </summary>
        public static readonly VoiceParameters John = new(VoiceName.John);

        /// <summary>
        /// Мужской русский голос
        /// </summary>
        public static readonly VoiceParameters Madirus = new(VoiceName.Madirus);

        /// <summary>
        /// Женский узбекский голос
        /// </summary>
        public static readonly VoiceParameters Nigora = new(VoiceName.Nigora);

        /// <summary>
        /// Название голоса. Подробнее см. список голосов
        /// </summary>
        public VoiceName Name { get; }

        /// <summary>
        /// Основной язык, который поддерживает голос.
        /// На этом языке разговаривал диктор при создании этого голоса.
        /// </summary>
        public VoiceLanguage Language { get; set; }

        /// <summary>
        /// Скорость (темп) синтезированной речи.
        /// Скорость речи задается дробным числом в диапазоне от 0.1 до 3.0
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// Амплуа или эмоциональная окраска голоса. Поддерживается только при выборе русского языка.
        /// </summary>
        public VoiceEmotion Emotion { get; set; }

        /// <summary>
        /// Инициалзация параметров голоса для генерации речи
        /// </summary>
        /// <param name="name">Название голоса</param>
        public VoiceParameters(
            VoiceName name,
            VoiceLanguage language = null,
            string speed = null,
            VoiceEmotion emotion = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Language = language;
            Speed = speed;
            Emotion = emotion;
        }
    }
}