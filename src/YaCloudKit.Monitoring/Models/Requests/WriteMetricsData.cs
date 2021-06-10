using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.Monitoring.Models.Requests
{
    /// <summary>
    /// Записываемая метрика
    /// </summary>
    public class WriteMetricsData
    {
        /// <summary>
        /// Обязательное поле. Имя метрики.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список меток метрики
        /// </summary>
        public Dictionary<string, string> Labels { get; set; }
        /// <summary>
        /// Тип метрики. Значение по умолчанию — DGAUGE
        /// <para>DGAUGE: Числовой показатель. Задается дробным числом.</para>
        /// <para>IGAUGE: Числовой показатель.Задается целым числом.</para>
        /// <para>COUNTER: Счетчик.</para>
        /// <para>RATE: Производная.</para>
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Временная метка. Если не указано, будет использовано текущее время.
        /// </summary>
        public DateTimeOffset? TimeStamp { get; set; }
        /// <summary>
        /// Обязательное поле. Значение метрики в указанной точке.
        /// </summary>
        public double Value { get; set; }

        public WriteMetricsData() { }
        public WriteMetricsData(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public WriteMetricsData AddLabel(string key, string value)
        {
            if (Labels == null)
                Labels = new Dictionary<string, string>();
            Labels.Add(key, value);
            return this;
        }

        public WriteMetricsData SetTimeStamp(DateTimeOffset time)
        {
            TimeStamp = time;
            return this;
        }

        public WriteMetricsData IsDgauge()
        {
            Type = "DGAUGE";
            return this;
        }

        public WriteMetricsData IsIgauge()
        {
            Type = "IGAUGE";
            return this;
        }

        public WriteMetricsData IsCounter()
        {
            Type = "COUNTER";
            return this;
        }

        public WriteMetricsData IsRate()
        {
            Type = "RATE";
            return this;
        }
    }
}
