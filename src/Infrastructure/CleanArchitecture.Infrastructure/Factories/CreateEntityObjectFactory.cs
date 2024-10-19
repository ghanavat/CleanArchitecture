using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;
using System.Reflection;
using CleanArchitecture.Shared.Attributes;

namespace CleanArchitecture.Infrastructure.Factories;

/// <inheritdoc/>
internal class CreateEntityObjectFactory<TRequest, TResponse>
    : IDomainFactory<TRequest, TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    public TResponse? CreateEntityObject(TRequest instance)
    {
        var method = GetMethod();
        if (method is null)
        {
            return null;
        }

        var constructorInfo = GetConstructor();
        if (constructorInfo is null)
        {
            return null;
        }
        
        var parameters = PopulateParameterValues(instance);
        if (parameters.Length == 0)
        {
            return null;
        }
        
        var entityObject = method.Invoke(constructorInfo.Invoke(null), parameters);

        return (TResponse?)entityObject;
    }
    
    private static MethodInfo? GetMethod()
    {
        var method = typeof(TResponse)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .FirstOrDefault(x => x.GetCustomAttribute<FactoryMethodAttribute>() is not null
                                 && x.GetCustomAttribute<FactoryMethodAttribute>()?.FactoryMethodName.ToString() == typeof(TResponse).Name);

        return method ?? null;
    }

    private static ConstructorInfo? GetConstructor()
    {
        return typeof(TResponse).GetConstructor(Type.EmptyTypes);
    }
    
    private static object[] PopulateParameterValues(TRequest request)
    {
        var properties = typeof(TRequest).GetProperties().ToList();

        if (properties.Count == 0)
        {
            return [];
        }
        
        var objValues = new object[properties.Count];
        for (var i = 0; i < properties.Count; i++)
        {
            objValues[i] = properties[i].GetValue(request)!;

            CheckInvariants(properties[i].Name, objValues[i].ToString());
        }

        return objValues;
    }

    private static void CheckInvariants(string? propertyName, string? propertyValue)
    {
        propertyName.CheckForNull(() => new ArgumentException("The property name passed in for invariant check is invalid.", nameof(propertyName)));
        propertyValue.CheckForNull();
    }
}
