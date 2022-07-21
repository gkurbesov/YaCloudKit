using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;
using YaCloudKit.MQ.Model;
using YaCloudKit.MQ.Model.Responses;

namespace YaCloudKit.MQ.Marshallers
{
    /// <summary>
    /// Базовый класс для демаршализации содержищий вспомогательные методы
    /// </summary>
    public class ResponseUnmarshaller : IUnmarshaller
    {
        /// <summary>
        /// Получить XML из стрима
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static XmlElement GetXmlElement(Stream stream)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            return doc.DocumentElement;
        }

        /// <summary>
        /// Поиск списка xml элементов
        /// </summary>
        /// <param name="mainElement">корнейвой XML элемент</param>
        /// <param name="xPath">Параметр для поиска запросов</param>
        /// <param name="list">коллекция найденых объектов</param>
        /// <returns>true - если найден хотя бы один элемент</returns>
        public static bool TryGetXmlElements(XmlNode mainElement, string xPath, out XmlNodeList list)
        {
            list = mainElement.SelectNodes(xPath);
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// Демаршалинг основных параметров ответа
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        public static void ResultUnmarshall(IResponseContext context, YandexMessageQueueResponse response)
        {
            response.ContentLength = context.ContentStream.Length;
            response.HttpStatusCode = context.StatusCode;
        }

        /// <summary>
        /// Демаршалинг атрибутов
        /// </summary>
        /// <param name="attributeList">коллекция содержащия XML объекты с атрибутами</param>
        /// <param name="values">Словарь, в который будут помещаться атрибуты</param>
        public static void AttributeUnmarshall(XmlNodeList attributeList, Dictionary<string, string> values)
        {
            foreach (XmlNode attrNode in attributeList)
            {
                var name = attrNode.SelectSingleNode("Name")?.InnerText;
                var value = attrNode.SelectSingleNode("Value")?.InnerText;
                if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(value))
                    values.Add(name, value);
            }
        }

        /// <summary>
        /// Демаршалинг масива с ошибками Batch-запросов
        /// </summary>
        /// <param name="batchList">коллекция содержащия XML объекты с ошибками</param>
        /// <param name="values">Лист, в который будут помещаться результаты ошибок</param>
        public static void BatchResultErrorEntryUnmarshaller(XmlNodeList batchList, List<BatchResultErrorEntry> values)
        {
            foreach (XmlNode item in batchList)
            {
                var senderFaultText = item.SelectSingleNode("SenderFault")?.InnerText;
                var senderFault = !string.IsNullOrWhiteSpace(senderFaultText)
                    ? (bool) Convert.ChangeType(senderFaultText, typeof(bool), CultureInfo.InvariantCulture)
                    : false;

                BatchResultErrorEntry errorEntry = new BatchResultErrorEntry()
                {
                    Code = item.SelectSingleNode("Code")?.InnerText,
                    Id = item.SelectSingleNode("Id")?.InnerText,
                    Message = item.SelectSingleNode("Message")?.InnerText,
                    SenderFault = senderFault
                };
                values.Add(errorEntry);
            }
        }

        /// <summary>
        /// Демаршалинг результата удаления пачки сообщений
        /// </summary>
        /// <param name="batchList">Коллекция содержащия XML объекты с результатами</param>
        /// <param name="values">Лист, в который будут помещаться результаты</param>
        public static void DeleteMessageBatchResultEntryUnmarshaller(XmlNodeList batchList,
            List<DeleteMessageBatchResultEntry> values)
        {
            foreach (XmlNode item in batchList)
            {
                var id = item.SelectSingleNode("Id")?.InnerText;
                if (!string.IsNullOrWhiteSpace(id))
                    values.Add(new DeleteMessageBatchResultEntry() {Id = id});
            }
        }

        /// <summary>
        /// Демаршалинг результата изменения видимости пачки сообщений
        /// </summary>
        /// <param name="batchList">Коллекция содержащия XML объекты с результатами</param>
        /// <param name="values">Лист, в который будут помещаться результаты</param>
        public static void ChangeMessageVisibilityBatchResultEntryUnmarshaller(XmlNodeList batchList,
            List<ChangeMessageVisibilityBatchResultEntry> values)
        {
            foreach (XmlNode item in batchList)
            {
                var id = item.SelectSingleNode("Id")?.InnerText;
                if (!string.IsNullOrWhiteSpace(id))
                    values.Add(new ChangeMessageVisibilityBatchResultEntry() {Id = id});
            }
        }

        /// <summary>
        /// Демаршалинг пользовательских атрибутов сообщения
        /// </summary>
        /// <param name="attributeList">Коллекция содержащия XML объекты с атрибутами</param>
        /// <param name="values">Словарь, в который будут помещаться объекты атрибутов</param>
        public static void MessageAttributeUnmarshall(XmlNodeList attributeList,
            Dictionary<string, MessageAttributeValue> values)
        {
            foreach (XmlNode attrNode in attributeList)
            {
                var attrName = attrNode.SelectSingleNode("Name")?.InnerText;
                var binaryValue = attrNode.SelectSingleNode("Value/BinaryValue")?.InnerText;
                var dataType = attrNode.SelectSingleNode("Value/DataType")?.InnerText;
                var stringValue = attrNode.SelectSingleNode("Value/StringValue")?.InnerText;

                if (!string.IsNullOrWhiteSpace(attrName) && !string.IsNullOrWhiteSpace(dataType))
                {
                    var messgaeAttr = new MessageAttributeValue()
                    {
                        DataType = (AttributeValueType) Enum.Parse(typeof(AttributeValueType), dataType, true)
                    };
                    switch (messgaeAttr.DataType)
                    {
                        case AttributeValueType.Binary:
                            if (!string.IsNullOrWhiteSpace(binaryValue))
                            {
                                messgaeAttr.BinaryValue = Convert.FromBase64String(binaryValue);
                                values.Add(attrName, messgaeAttr);
                            }

                            break;
                        default:
                            if (!string.IsNullOrWhiteSpace(stringValue))
                            {
                                messgaeAttr.StringValue = stringValue;
                                values.Add(attrName, messgaeAttr);
                            }

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Демаршалинг ошибки
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static YandexMqServiceException ErrorUnmarshall(IResponseContext context)
        {
            try
            {
                var rootNode = GetXmlElement(context.ContentStream);
                var errorNode = rootNode.SelectSingleNode("Error");
                var requestIdNode = rootNode.SelectSingleNode("RequestId");

                var errorType = errorNode.SelectSingleNode("Type")?.InnerText ?? string.Empty;
                var errorCode = errorNode.SelectSingleNode("Code")?.InnerText ?? string.Empty;
                var errorMessage = errorNode.SelectSingleNode("Message")?.InnerText ?? string.Empty;
                var requestId = requestIdNode.InnerText;

                return new YandexMqServiceException(errorMessage, errorType, errorCode, requestId, context.StatusCode);
            }
            catch (Exception ex)
            {
                var message = new StreamReader(context.ContentStream).ReadToEnd();
                YandexMqServiceException exception = new YandexMqServiceException(message, ex)
                {
                    StatusCode = context.StatusCode
                };
                return exception;
            }
        }

        /// <summary>
        /// Демаршалинг ошибки
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        public static YandexMqServiceException ErrorUnmarshall(string message, Exception innerException,
            HttpStatusCode httpStatusCode)
        {
            YandexMqServiceException exception = new YandexMqServiceException(message, innerException)
            {
                StatusCode = httpStatusCode
            };
            return exception;
        }

        public virtual T Unmarshall<T>(IResponseContext context) where T : YandexMessageQueueResponse, new()
        {
            throw new NotImplementedException();
        }

        public YandexMqServiceException UnmarshallException(IResponseContext context)
        {
            try
            {
                return ErrorUnmarshall(context);
            }
            catch (Exception ex)
            {
                return ErrorUnmarshall(ex.Message, ex, context.StatusCode);
            }
        }
    }
}