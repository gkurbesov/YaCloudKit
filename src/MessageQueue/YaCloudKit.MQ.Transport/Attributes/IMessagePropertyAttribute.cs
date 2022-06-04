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
