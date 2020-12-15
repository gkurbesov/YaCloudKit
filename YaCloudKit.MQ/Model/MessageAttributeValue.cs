namespace YaCloudKit.MQ.Model
{
    /// <summary>
    /// Пользовательский атрибут сообщения
    /// </summary>
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

        internal bool IsSetValue()
        {
            switch (DataType)
            {
                case AttributeValueType.Binary:
                    return BinaryValue != null && BinaryValue.Length > 0;
                default:
                    return !string.IsNullOrWhiteSpace(StringValue);
            }
        }
    }
}
