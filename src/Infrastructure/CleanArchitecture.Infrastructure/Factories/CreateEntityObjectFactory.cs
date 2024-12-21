using System.Collections.Immutable;
using System.Reflection;
using CleanArchitecture.Core.ActionOptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Attributes;
using CleanArchitecture.Shared.Extensions;

namespace CleanArchitecture.Infrastructure.Factories;

/// <inheritdoc/>
internal class CreateEntityObjectFactory<TRequest, TResponse>
    : IDomainFactory<TRequest, TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    public TResponse? CreateEntityObject(TRequest request, Action<DomainFactoryOption>? action)
    {
        var domainFactoryOption = new DomainFactoryOption();
        if (action is not null)
        {
            domainFactoryOption = new DomainFactoryOption();
            action(domainFactoryOption);
        }

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

        var parameters = PopulateParameterValues(request,
            domainFactoryOption.PropertyInfoItems,
            domainFactoryOption.AdditionalProperties);

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
                                 && x.GetCustomAttribute<FactoryMethodAttribute>()?.FactoryMethodName.ToString() ==
                                 typeof(TResponse).Name);

        return method ?? null;
    }

    private static ConstructorInfo? GetConstructor()
    {
        return typeof(TResponse).GetConstructor(Type.EmptyTypes);
    }

    /// <summary>
    /// Populates an argument list for the constructor to be invoked.
    /// </summary>
    /// <param name="request">The request type from which the parameter types and values are retrieved from</param>
    /// <param name="ignoredProperties">List of the properties to be ignored from the iteration</param>
    /// <param name="additionalProperties">List of the properties to be added to the iteration</param>
    /// <returns>Object array of parameter values in the order they were defined in the request type</returns>
    private static object[] PopulateParameterValues(TRequest request,
        IImmutableList<string> ignoredProperties,
        IImmutableDictionary<string, object> additionalProperties)
    {
        var properties = typeof(TRequest).GetProperties().ToList();

        if (properties.Count == 0)
        {
            return [];
        }
        
        RemoveIgnoredProperty(ignoredProperties, properties);

        var objValues = new object[properties.Count];
        for (var i = 0; i < objValues.Length; i++)
        {
            objValues[i] = properties[i].GetValue(request).CheckForNull();

            if (properties[i].IsValueTypeNullable()
                && properties[i].IsReferenceTypeNullable())
            {
                continue;
            }

            var propertyName = properties[i].Name;

            objValues[i].CheckForNull(() =>
                new NullReferenceException($"Value of property {propertyName} cannot be null or empty."));
        }

        return additionalProperties.Count <= 0 ? 
            objValues 
            : additionalProperties.Aggregate(objValues, (current, item) => current.Append(item.Value).ToArray());

        void RemoveIgnoredProperty(IImmutableList<string> ignoredPropertyList, List<PropertyInfo> propertySourceList)
        {
            foreach (var ignoredPropertyItem in ignoredPropertyList)
            {
                var index = propertySourceList.FindIndex(x => x.Name == ignoredPropertyItem);
                propertySourceList.RemoveAt(index);
            }
        }
    }
}
