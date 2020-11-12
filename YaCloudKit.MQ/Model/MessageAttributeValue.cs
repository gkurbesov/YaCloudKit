using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Model
{
    public class MessageAttributeValue
    {
        /// <summary>
        /// Любые двоичные данные.
        /// </summary>
        public byte[] BinaryValue { get; set; }
        /// <summary>
        /// Тип данных: String, Number или Binary. Для типа Number нужно использовать StringValue.
        /// </summary>
        public AttributeValueType DataType { get; set; }
        /// <summary>
        /// Строка в кодировке UTF-8.
        /// </summary>
        public string StringValue { get; set; }
    }
}
