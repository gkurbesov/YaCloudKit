using System;
using System.Collections.Generic;
using System.Text;

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
        public string iamToken { get; set; }
        /// <summary>
        /// Время окончания действия IAM-токена. Строка в формате RFC3339.
        /// </summary>
        public string expiresAt { get; set; }
    }
}
