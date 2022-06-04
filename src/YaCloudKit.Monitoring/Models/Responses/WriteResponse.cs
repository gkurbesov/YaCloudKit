namespace YaCloudKit.Monitoring.Models.Responses
{
    /// <summary>
    /// Результат записи метрик
    /// </summary>
    public class WriteResponse
    {
        /// <summary>
        /// Количество успешно записанных метрик
        /// </summary>
        public string WrittenMetricsCount { get; set; }
        /// <summary>
        /// Сообщение об ошибке, если запись завершилась неуспешно
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
