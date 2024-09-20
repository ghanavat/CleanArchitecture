using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Extensions;
using CleanArchitecture.Template.Shared.Attributes;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Factories;

/// <inheritdoc/>
internal class CreateEntityObjectFactory<TRequest, TResponse>
    : IDomainFactory<TRequest, TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    public TResponse? CreateEntityObject(TRequest instance)
    {
        #region To use constructor as a factory - Remove when you are sure
        //var constructorInfo = typeof(TResponse)
        //    .GetConstructor(BindingFlags.NonPublic
        //        | BindingFlags.Instance
        //        | BindingFlags.Public
        //        | BindingFlags.Static,
        //        PopulateTypeObjectsForProperties(requestProperties)) ?? throw new Exception("There has been a problem with your request.");

        //var constructor = constructorInfo.Invoke(PopulateProperyValues(requestProperties, instance));

        //if (!constructor.GetType().Equals(typeof(TResponse))
        //    || constructor is null)
        //{
        //    throw new Exception("There has been a problem with your request.");
        //}

        //var customAttributes = Attribute.GetCustomAttributes(GetType());

        //foreach (var attrItem in customAttributes)
        //{
        //    if (attrItem is FactoryMethodAttribute factoryMethodAttribute)
        //    {
        //        //factoryMethodAttribute.FactoryMethodName;
        //    }
        //}

        #endregion

        var method = typeof(TResponse)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .FirstOrDefault(x => x.GetCustomAttribute<FactoryMethodAttribute>() is not null
                                 && x.GetCustomAttribute<FactoryMethodAttribute>()?.FactoryMethodName.ToString() == typeof(TResponse).Name);

        if (method is null)
        {
            return null;
        }

        var constructorInfo = typeof(TResponse).GetConstructor(Type.EmptyTypes);
        if (constructorInfo is null)
        {
            return null;
        }

        var baseType = constructorInfo.GetType().BaseType;
        if (baseType is null)
        {
            return null;
        }
        else if (baseType.Equals(typeof(EntityBase)))
        {
            
        }

        var requestProperties = typeof(TRequest).GetProperties();

        var constructorInstance = constructorInfo.Invoke(null);
        var entityObject = method.Invoke(constructorInstance, PopulatePropertyValues(requestProperties.ToList(), instance));

        return (TResponse?)entityObject;
    }

    //private static Type[] PopulateTypeObjectsForProperties(PropertyInfo[] properties)
    //{
    //    Type[] types = new Type[properties.Length];
    //    for (int i = 0; i < properties.Length; i++)
    //    {
    //        types[i] = properties[i].PropertyType;
    //    }

    //    return types;
    //}

    /// <summary>
    /// Populates an array of property values in order from the request command
    /// </summary>
    /// <param name="properties">List of properties as PropertyInfo</param>
    /// <param name="sourceType">The source type whose property values will be returned</param>
    /// <returns>An object array of property values</returns>
    private static object[] PopulatePropertyValues(IList<PropertyInfo> properties, TRequest sourceType)
    {
        var objValues = new object[properties.Count];
        for (var i = 0; i < properties.Count; i++)
        {
            objValues[i] = properties[i].GetValue(sourceType)!;

            CheckInvariants(properties[i].Name, objValues[i].ToString());
        }

        return objValues;
    }

    private static void CheckInvariants(string? propertyName, string? propertyValue)
    {
        propertyName.CheckForNull(() => throw new ArgumentException("The property name passed in for invariant check is invalid.", nameof(propertyName)));
        propertyValue.CheckForNull();
    }
}
