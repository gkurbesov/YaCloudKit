using System;

namespace YaCloudKit.TTS
{
    public static class RequestParametersHelper
    {
        public static void AddTextParam(IRequestContext context, string text, bool ssml)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (text.Length > 5000)
                throw new ArgumentOutOfRangeException(nameof(text),
                    "The maximum text length must not exceed 5000 characters");

            context.AddParameter(ssml ? "ssml" : "text", text);
        }

        public static void AddVoiceParam(IRequestContext context, VoiceParameters voice)
        {
            if (voice == null)
                throw new ArgumentNullException(nameof(voice));

            context.AddParameter("voice", voice.Name);

            if (voice.Language is not null)
                context.AddParameter("lang", voice.Language);
            if (voice.Emotion is not null)
                context.AddParameter("emotion", voice.Emotion);
            if (voice.Emotion is not null)
                context.AddParameter("speed", voice.Speed);
        }

        public static void AddFormatParam(IRequestContext context, AudioFormat format)
        {
            if (format == null || string.IsNullOrWhiteSpace(format.Format))
                throw new ArgumentException(nameof(format));

            context.AddParameter("format", format.Format);
            if (format.SampleRateHertz.HasValue)
                context.AddParameter("sampleRateHertz", format.SampleRateHertz.ToString());
        }

        public static void AddFolderParam(IRequestContext context, YandexTtsConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (!string.IsNullOrWhiteSpace(config.TokenIAM))
            {
                if (string.IsNullOrWhiteSpace(config.FolderID))
                    throw new ArgumentNullException(nameof(config.FolderID));

                context.AddParameter("folderId", config.FolderID);
            }
        }
    }
}