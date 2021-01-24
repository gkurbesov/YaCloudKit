using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.IAM
{
    public static class TokenRecipientExtension
    {
        public static async Task<IamTokenCreateResult> GetIamTokenWithJwt(this ITokenRecipient client, string jwt, CancellationToken cancellationToken = default)
        {
            return await client.GetIamToken(TokenRecipientOptions.WithJwtToken(jwt), cancellationToken);
        }
        public static async Task<IamTokenCreateResult> GetIamTokenWithOAuth(this ITokenRecipient client, string token, CancellationToken cancellationToken = default)
        {
            return await client.GetIamToken(TokenRecipientOptions.WithOAuthToken(token), cancellationToken);
        }Ht
    }
}
