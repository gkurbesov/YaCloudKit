using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.Core;
using YaCloudKit.TTS.Model;

namespace YaCloudKit.TTS.Utils
{
    public class RequestParametersHelper
    {
        public static void AddTextParam(IRequestContext context, string text, bool ssml)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (text.Length > 5000)
                throw new ArgumentOutOfRangeException(nameof(text), "Максимальная длина текста не должна превышать 5000 символов");

            context.AddParametr(ssml ? "ssml" : "text", text);
        }

        public static void AddVoiceParam(IRequestContext context, VoiceParameters voice)
        {
            if (voice == null || !voice.IsSetParam())
                throw new ArgumentException(nameof(voice));

            context.AddParametr("lang", voice.Language);
            context.AddParametr("voice", voice.Name);
            if (!string.IsNullOrWhiteSpace(voice.Emotion))
                context.AddParametr("emotion", voice.Emotion);
            if (!string.IsNullOrWhiteSpace(voice.Emotion))
                context.AddParametr("speed", voice.Speed);
        }

        public static void AddFormatParam(IRequestContext context, FormatParameters format)
        {
            if (format == null || string.IsNullOrWhiteSpace(format.Format))
                throw new ArgumentNullException(nameof(format));

            context.AddParametr("format", format.Format);
            if (format.SampleRateHertz.HasValue)
                context.AddParametr("sampleRateHertz", format.SampleRateHertz.ToString());
        }

        public static void AddFolderParam(IRequestContext context, YandexTtsConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (!string.IsNullOrWhiteSpace(config.TokenIAM))
            {
                if (string.IsNullOrWhiteSpace(config.FolderID))
                    throw new ArgumentNullException(nameof(config.FolderID));

                context.AddParametr("folderId", config.FolderID);
            }
        }
    }
}
