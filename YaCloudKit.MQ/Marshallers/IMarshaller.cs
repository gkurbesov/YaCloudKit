namespace YaCloudKit.MQ.Marshallers
{
    /// <summary>
    /// Интерфейс для маршаллеров, которые преобразуют объекты запросов в данные для отправки по HTTP
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMarshaller<T>
    {
        IRequestContext Marshall(T input);
    }
}
