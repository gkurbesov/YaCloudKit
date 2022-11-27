using System.Net;
using YaCloudKit.TTS;

const string apiKey = "[api-key]";
const string text = "Привет! Меня зовут Майк, а это пример синтеза речи!";
const string fileName = "tts_test.mp3";

// Yandex.Cloud return request id when LoggingEnabled = true
IYandexTts client = new YandexTtsClient(new YandexTtsConfig(apiKey) {LoggingEnabled = true});

try
{
    var result = await client.TextToSpeechAsync(text, VoiceParameters.Ermil, AudioFormat.Mp3);

    Console.WriteLine("Status code: " + result.StatusCode);
    Console.WriteLine("Request id: " + result.RequestId);
    
    if (File.Exists(fileName))
        File.Delete(fileName);

    File.WriteAllBytes(fileName, result.Content);

    Console.WriteLine("File saved: " + fileName);
}
catch (YandexTtsServiceException e)
{
    
    Console.WriteLine("Status code: " + e.StatusCode);
    Console.WriteLine("Request id: " + e.RequestId);
    Console.WriteLine(e.Message);
}