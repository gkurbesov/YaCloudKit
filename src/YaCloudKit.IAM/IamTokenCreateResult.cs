namespace YaCloudKit.IAM
{
    /// <summary>
    /// Результат запроса на создание нового IAM-токена
    /// </summary>
    public class IamTokenCreateResult
    {
        /// <summary>
        /// IAM-токен
        /// </summary>
        public string IamToken { get; set; }
        /// <summary>
        /// Время окончания действия IAM-токена. Строка в формате RFC3339.
        /// </summary>
        public string ExpiresAt { get; set; }
    }
}
