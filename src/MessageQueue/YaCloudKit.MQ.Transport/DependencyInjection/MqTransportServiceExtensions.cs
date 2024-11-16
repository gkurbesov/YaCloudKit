using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Transport.DependencyInjection;

public static class MqTransportServiceExtensions
{
    public static IServiceCollection AddMqTransport(
        this IServiceCollection services,
        Action<MessageConverterComponentOptionsBuilder> configuration,
        params Assembly[] assembliesToScan)
    {
        return services
            .AddMessageConverters(configuration)
            .AddMessageConverterComponent()
            .AddHandlersFromAssemblies(assembliesToScan)
            .AddMqTransportService();
    }
    
    public static IServiceCollection AddMqTransport(
        this IServiceCollection services,
        Action<IServiceProvider, MessageConverterComponentOptionsBuilder> configuration,
        params Assembly[] assembliesToScan)
    {
        return services
            .AddMessageConverters(configuration)
            .AddMessageConverterComponent()
            .AddHandlersFromAssemblies(assembliesToScan)
            .AddMqTransportService();
    }

    public static IServiceCollection AddMqTransportService(this IServiceCollection services)
    {
        return services.AddSingleton<IMqTransportService>(sp =>
            new MqTransportService(
                sp.GetRequiredService<IMessageConverterComponent>(),
                sp.GetRequiredService));
    }

    public static IServiceCollection AddHandlersFromAssemblies(
        this IServiceCollection services,
        params Assembly[] assembliesToScan)
    {
        var handlerInterfaceType = typeof(IMessageHandler<>);

        foreach (var concretionType in assembliesToScan
                     .SelectMany(a => a.DefinedTypes)
                     .Where(t => t.IsConcrete() && !t.IsOpenGeneric()))
        {
            var interfaceTypes = concretionType
                .GetTypeInfo()
                .GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == handlerInterfaceType)
                .ToArray();

            if (!interfaceTypes.Any()) continue;

            foreach (var interfaceType in interfaceTypes)
            {
                services.AddTransient(interfaceType, concretionType);
            }
        }

        return services;
    }

    public static IServiceCollection AddMessageConverters(
        this IServiceCollection services,
        Action<IServiceProvider, MessageConverterComponentOptionsBuilder> configuration)
    {
        var builder = new MessageConverterComponentOptionsBuilder();
        return services.AddSingleton(sp =>
        {
            configuration(sp, builder);
            return builder.Build();
        });
    }

    public static IServiceCollection AddMessageConverters(
        this IServiceCollection services,
        Action<MessageConverterComponentOptionsBuilder> configuration)
    {
        var builder = new MessageConverterComponentOptionsBuilder();
        configuration(builder);
        return services.AddSingleton(builder.Build());
    }

    public static IServiceCollection AddMessageConverterComponent(this IServiceCollection services)
    {
        return services.AddSingleton<IMessageConverterComponent, MessageConverterComponent>();
    }
}