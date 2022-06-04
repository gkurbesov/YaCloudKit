using System;
using System.Collections.Generic;

namespace YaCloudKit.Monitoring.Models.Requests
{
    /// <summary>
    /// Тело запроса на запись метрик
    /// </summary>
    public class WriteRequest
    {
        /// <summary>
        /// Общая временная метка для всех метрик
        /// </summary>
        public DateTimeOffset? TimeStamp { get; set; }
        /// <summary>
        /// Список меток общих для всех передаваемых метрик
        /// </summary>
        public Dictionary<string, string> Labels { get; set; }
        /// <summary>
        /// Список метрик
        /// </summary>
        public List<WriteMetricsData> Metrics { get; set; } = new List<WriteMetricsData>();


        public WriteRequest AddLabel(string key, string value)
        {
            if (Labels == null)
                Labels = new Dictionary<string, string>();
            Labels.Add(key, value);
            return this;
        }
        public WriteRequest SetTimeStamp(DateTimeOffset time)
        {
            TimeStamp = time;
            return this;
        }

        public WriteRequest AddMetric(WriteMetricsData value)
        {
            Metrics.Add(value);
            return this;
        }

        public WriteRequest AddMetric(string name, double value)
        {
            Metrics.Add(new WriteMetricsData(name, value));
            return this;
        }
    }
}
