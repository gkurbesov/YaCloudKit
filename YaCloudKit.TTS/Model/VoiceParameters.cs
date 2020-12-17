using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.TTS.Model
{
    /// <summary>
    /// Параметры голоса для генерации
    /// </summary>
    public class VoiceParameters
    {
        /// <summary>
        /// Женский русский голос Oksana
        /// </summary>
        public static readonly VoiceParameters Oksana = new VoiceParameters("oksana", "ru-RU");
        /// <summary>
        /// Женский русский голос Jane
        /// </summary>
        public static readonly VoiceParameters Jane = new VoiceParameters("jane", "ru-RU");
        /// <summary>
        /// Женский русский голос Omazh
        /// </summary>
        public static readonly VoiceParameters Omazh = new VoiceParameters("omazh", "ru-RU");
        /// <summary>
        /// Мужской русский голос Zahar
        /// </summary>
        public static readonly VoiceParameters Zahar = new VoiceParameters("zahar", "ru-RU");
        /// <summary>
        /// Мужской русский голос Ermil
        /// </summary>
        public static readonly VoiceParameters Ermil = new VoiceParameters("ermil", "ru-RU");
        /// <summary>
        /// Женский турецкий голос Silaerkan
        /// </summary>
        public static readonly VoiceParameters Silaerkan = new VoiceParameters("silaerkan", "tr-TR");
        /// <summary>
        /// Мужской турецкий голос Erkanyavas
        /// </summary>
        public static readonly VoiceParameters Erkanyavas = new VoiceParameters("erkanyavas", "tr-TR");
        /// <summary>
        /// Женский английский голос Alyss
        /// </summary>
        public static readonly VoiceParameters Alyss = new VoiceParameters("alyss", "en-US");
        /// <summary>
        /// Мужской английский голос Nick
        /// </summary>
        public static readonly VoiceParameters Nick = new VoiceParameters("nick", "en-US");
        /// <summary>
        /// Женский русский голос Alena (Премиум)
        /// </summary>
        public static readonly VoiceParameters PremiumAlena = new VoiceParameters("alena", "ru-RU");
        /// <summary>
        /// Мужской русский голос Filipp (Премиум)
        /// </summary>
        public static readonly VoiceParameters PremiumFilipp = new VoiceParameters("filipp", "ru-RU");


        /// <summary>
        /// Название голоса. Подробнее см. список голосов
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Основной язык, который поддерживает голос. На этом языке разговаривал диктор при создании этого голоса.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Скорость (темп) синтезированной речи. Для премиум-голосов временно не поддерживается.
        /// Скорость речи задается дробным числом в диапазоне от 0.1 до 3.0
        /// </summary>
        public float Speed { get; set; } = 1.0f;
        /// <summary>
        /// Эмоциональная окраска голоса. Поддерживается только при выборе русского языка (ru-RU) и голосов jane или omazh.
        /// Допустимые значения: good, evil, neutral
        /// </summary>
        public string Emotion { get; set; }

        /// <summary>
        /// Инициалзация параметров голоса для генерации речи
        /// </summary>
        /// <param name="name">Название голоса</param>
        /// <param name="language">Язык произношения</param>
        public VoiceParameters(string name, string language)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentNullException(nameof(language));
            Language = language;
        }
    }
}
