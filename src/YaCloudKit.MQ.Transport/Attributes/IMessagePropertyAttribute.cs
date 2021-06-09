using System;
using System.Collections.Generic;
using System.Text;

namespace YaCloudKit.MQ.Transport.Attributes
{
    /// <summary>
    /// Интерфейс атрибутов помечаемых сообщений
    /// </summary>
    public interface IMessagePropertyAttribute
    {
        string Name { get; }
    }
}
