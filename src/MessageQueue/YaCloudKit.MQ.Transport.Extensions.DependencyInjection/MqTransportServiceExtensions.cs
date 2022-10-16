using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace YaCloudKit.MQ.Transport.Extensions.DependencyInjection;

public static class MqTransportServiceExtensions
{
    public static IServiceCollection AddHandlersFromAssemblies(
        this IServiceCollection services,
        IEnumerable<Assembly> assembliesToScan)
    {
        var handlerType = typeof(IMessageHandler<>);
        var concretions = assembliesToScan
            .SelectMany(a => a.DefinedTypes)
            .Where(t => t.IsConcrete() && t.IsOpenGeneric())
            .ToArray();

        foreach (var type in concretions)
        {
            services.AddTransient(handlerType, type);
        }
        
        return services;
    }
    
    public static IServiceCollection AddMessageConverterComponentOptions(
        this IServiceCollection services,
        Action<IServiceProvider, MessageConverterComponentOptionsBuilder>  configuration)
    {
        var builder = new MessageConverterComponentOptionsBuilder();
        return services.AddSingleton(sp =>
        {
            configuration(sp, builder);
            return builder.Build();
        });
    }
    
    public static IServiceCollection AddMessageConverterComponentOptions(
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

    public static IServiceCollection AddMqTransportService(this IServiceCollection services)
    {
        return services.AddSingleton<IMqTransportService>(sp =>
            new MqTransportService(
                sp.GetRequiredService<IMessageConverterComponent>(),
                sp.GetRequiredService));
    }
    
    private static bool IsConcrete(this Type type)
    {
        return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
    }
    
    private static bool IsOpenGeneric(this Type type)
    {
        return type.GetTypeInfo().IsGenericTypeDefinition || type.GetTypeInfo().ContainsGenericParameters;
    }
}