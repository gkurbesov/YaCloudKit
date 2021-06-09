using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace YaCloudKit.MQ.Transport.Converters
{
    /// <summary>
    /// Стандартный XML конвертер
    /// </summary>
    public class XmlMessageConverter : IMessageConverter
    {
        /// <summary>
        /// Название для конвертера
        /// </summary>
        public const string TAG = "xml";

        public T Deserialize<T>(string messageBody) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(messageBody)))
            {
                var result = serializer.Deserialize(ms) as T;
                return result;
            }
        }

        public object Deserialize(string messageBody, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(messageBody)))
            {
                var result = serializer.Deserialize(ms);
                return result;
            }
        }

        public string Serialize(object value)
        {
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, value);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
