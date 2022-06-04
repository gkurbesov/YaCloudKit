using System;

namespace YaCloudKit.MQ.Transport.Attributes
{
    /// <summary>
    /// Атрибут с именем типа объекта
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MessageQueueNameAttribute : Attribute, IMessagePropertyAttribute
    {
        /// <summary>
        /// Имя типа объекта, который будет сериализоваться
        /// </summary>
        public string Name { get; set; }

        public MessageQueueNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
