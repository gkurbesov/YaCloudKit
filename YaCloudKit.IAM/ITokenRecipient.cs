using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YaCloudKit.IAM
{
    public interface ITokenRecipient
    {
        Task<string> GetIamToken(TokenRecipientOptions options, CancellationToken cancellationToken = default);
    }
}
