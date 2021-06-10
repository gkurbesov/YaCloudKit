using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YaCloudKit.Monitoring.Models.Requests;
using YaCloudKit.Monitoring.Models.Responses;

namespace YaCloudKit.Monitoring
{
    public interface IMonitoringClient
    {
        Task<WriteResponse> Write(WriteRequest request, CancellationToken cancellationToken = default);
    }
}
