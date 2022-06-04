using System;

namespace YaCloudKit.MQ.Transport.Attributes
{
    /// <summary>
    /// Класс для работы с атрибутами
    /// </summary>
    public class AttributeHelper
    {
        public static string GetPropertyName<T>(Type value, bool defaultValue = false) where T : IMessagePropertyAttribute
        {
            var attributes = value.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr is T messageAttribute)
                {
                    return messageAttribute.Name;
                }
            }
            return defaultValue ? value.Name : null;
        }

        /// <summary>
        /// Возвращает значение атрибута
        /// </summary>
        /// <typeparam name="T">Тип искомого атрибута</typeparam>
        /// <param name="value">экземпляр объекта</param>
        /// <param name="defaultValue">Возвращать ли значение по умолчанию, если true - вернят имя класса в случае елси атрибут не найден</param>
        /// <returns></returns>
        public static string GetPropertyName<T>(object value, bool defaultValue = false) where T : IMessagePropertyAttribute =>
            GetPropertyName<T>(value.GetType(), defaultValue);
    }
}
