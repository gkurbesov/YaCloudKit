using System;
using System.Collections.Generic;
using System.Text;
using YaCloudKit.MQ.Model;

namespace YaCloudKit.MQ.Utils
{
    public static class RequestAttributesBuilder
    {
        public static void NamedAttributes(IRequestContext context, Dictionary<string, string> values)
        {
            var number = 1;
            foreach(var item in values)
            {
                context.AddParametr($"Attribute.{number}.Name", item.Key);
                context.AddParametr($"Attribute.{number}.Value", item.Value);
                number++;
            }
        }

        public static void MessageAttributes(IRequestContext context, Dictionary<string, MessageAttributeValue> values)
        {
            var number = 1;
            foreach (var item in values)
            {
                if (item.Value.IsSetValue())
                {
                    context.AddParametr($"MessageAttribute.{number}.Name", item.Key);
                    context.AddParametr($"MessageAttribute.{number}.Value.DataType", item.Value.DataType.ToString());
                    switch (item.Value.DataType)
                    {
                        case AttributeValueType.Binary:
                            context.AddParametr($"MessageAttribute.{number}.Value.BinaryValue", Convert.ToBase64String(item.Value.BinaryValue));
                            break;
                        default:
                            context.AddParametr($"MessageAttribute.{number}.Value.StringValue", item.Value.StringValue);
                            break;
                    }
                }
                number++;
            }
        }


        public static void MessageAttributesBatchEntry(int entryNumber, IRequestContext context, Dictionary<string, MessageAttributeValue> values)
        {
            var number = 1;
            foreach (var item in values)
            {
                if (item.Value.IsSetValue())
                {
                    context.AddParametr($"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.{number}.Name", item.Key);
                    context.AddParametr($"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.{number}.Value.DataType", item.Value.DataType.ToString());
                    switch (item.Value.DataType)
                    {
                        case AttributeValueType.Binary:
                            context.AddParametr($"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.{number}.Value.BinaryValue", Convert.ToBase64String(item.Value.BinaryValue));
                            break;
                        default:
                            context.AddParametr($"SendMessageBatchRequestEntry.{entryNumber}.MessageAttribute.{number}.Value.StringValue", item.Value.StringValue);
                            break;
                    }
                }
                number++;
            }
        }

        public static void ListAttributes(IRequestContext context, List<string> values)
        {
            var number = 1;
            foreach (var item in values)
            {
                context.AddParametr($"AttributeName.{number}", item);
                number++;
            }
        }

        public static void ListMessageAttributes(IRequestContext context, List<string> values)
        {
            var number = 1;
            foreach (var item in values)
            {
                context.AddParametr($"MessageAttributeName.{number}", item);
                number++;
            }
        }
    }
}
