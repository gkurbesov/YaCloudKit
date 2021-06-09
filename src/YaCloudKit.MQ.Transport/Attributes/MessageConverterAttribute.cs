using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    /// <summary>
    /// Атрибут с именем используемого конвертера
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MessageConverterAttribute : Attribute, IMessagePropertyAttribute
    {
        /// <summary>
        /// Имя конвертера, который используется для сериализации объекта
        /// </summary>
        public string Name { get; set; }

        public MessageConverterAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
