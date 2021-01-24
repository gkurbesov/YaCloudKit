using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.IAM
{
    public interface ITokenRecipient
    {
        Task<IamTokenCreateResult> GetIamToken(TokenRecipientOptions options, CancellationToken cancellationToken = default);
    }
}
