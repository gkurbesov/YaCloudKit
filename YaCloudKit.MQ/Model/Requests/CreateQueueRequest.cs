using System.Collections.Generic;

namespace YaCloudKit.MQ.Model.Requests
{
    /// <summary>
    /// Метод для создания новой стандартной или FIFO очереди.
    /// По умолчанию создается стандартная очередь.
    /// Чтобы создать очередь FIFO необходимо использовать параметр <code>FifoQueue</code>
    /// </summary>
    public class CreateQueueRequest : BaseRequest
    {
        /// <summary>
        /// Имя очереди. Максимальная длина — 80 символов. 
        /// В имени можно использовать цифры, буквы, нижние подчеркивания и дефисы.
        /// Имя очереди FIFO должно заканчиваться суффиксом <code>.fifo</code>
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// Список атрибутов очереди.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

        public CreateQueueRequest()
            : base("CreateQueue") { }
    }
}
