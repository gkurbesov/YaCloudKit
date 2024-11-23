using System.Net;
using System.Text.Json.Serialization;

namespace YaCloudKit.Postbox.Model.Responses;

public abstract record YandexPostboxBaseResponse
{
    [JsonIgnore]
    public HttpStatusCode? HttpStatusCode { get; internal set; }
}