namespace YaCloudKit.Postbox;

public static class YandexPostboxDefaults
{
    public static string PostboxApiHost { get; } = "https://postbox.cloud.yandex.net";
    public static string SendEmailUrl { get; } = "/v2/email/outbound-emails";
}