using Newtonsoft.Json;

namespace YaCloudKit.IAM.Utils
{
    public class JsonBodyHelper
    {
        public static string OauthBody(string token) => JsonConvert.SerializeObject(new { yandexPassportOauthToken = token }, Formatting.Indented);

        public static string JwtBody(string token) => JsonConvert.SerializeObject(new { jwt = token }, Formatting.Indented);
        public static IamTokenCreateResult DeserializeResult(string json) => JsonConvert.DeserializeObject<IamTokenCreateResult>(json);
    }
}
