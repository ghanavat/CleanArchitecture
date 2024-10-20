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
    
    /// <summary>
    /// The return type is defined as nullable to allow null value and reference type properties
    /// </summary>
    /// <param name="request">The request type from which the parameter values are retrieved from</param>
    /// <returns>Object array of parameter values in the order they were defined in the request type</returns>
    private static object?[] PopulateParameterValues(TRequest request)
    {
        var properties = typeof(TRequest).GetProperties().ToList();

        if (properties.Count == 0)
        {
            return [];
        }
        
        var objValues = new object?[properties.Count];
        for (var i = 0; i < properties.Count; i++)
        {
            if (!properties[i].IsValueTypeNullable() || !properties[i].IsReferenceTypeNullable())
            {
                var propertyName = properties[i].Name;
                objValues[i].CheckForNull(() => new NullReferenceException($"Value of property {propertyName} cannot be null or empty."));
            }
            
            objValues[i] = properties[i].GetValue(request);
        }

        return objValues;
    }
}
