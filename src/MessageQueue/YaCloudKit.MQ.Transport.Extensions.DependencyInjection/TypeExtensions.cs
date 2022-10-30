using System;
using System.Reflection;

namespace YaCloudKit.MQ.Transport.Extensions.DependencyInjection;

internal static class TypeExtensions
{
    internal static bool IsConcrete(this Type type)
    {
        var typeInfo = type.GetTypeInfo();
        return !typeInfo.IsAbstract && !typeInfo.IsInterface;
    }
    
    internal static bool IsOpenGeneric(this Type type)
    {
        var typeInfo = type.GetTypeInfo();
        return typeInfo.IsGenericTypeDefinition || typeInfo.ContainsGenericParameters;
    }
}