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

        var method = typeof(TResponse).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
            .Where(x => x.GetCustomAttribute<FactoryMethodAttribute>() is not null
                && x.GetCustomAttribute<FactoryMethodAttribute>()?.FactoryMethodName.ToString() == typeof(TResponse).Name)
            .FirstOrDefault();

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

        var constructorIntance = constructorInfo.Invoke(null);
        var entityObject = method.Invoke(constructorIntance, PopulateProperyValues(requestProperties, instance));

        return (TResponse?)entityObject;
    }

    public Task DeleteAsync(TRequest instance)
    {
        throw new NotImplementedException();
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
    /// <param name="properties">Array of properties as PropertyInfo</param>
    /// <param name="sourceType">The source type whose propety values will be returned</param>
    /// <returns>An object array of property values</returns>
    private static object[] PopulateProperyValues(IEnumerable<PropertyInfo> properties, TRequest sourceType)
    {
        var listProps = properties.ToList();

        object[] objValues = new object[properties.Count()];
        for (int i = 0; i < objValues.Length; i++)
        {
            objValues[i] = listProps[i].GetValue(sourceType)!;

            CheckInvariants(listProps[i].Name, objValues[i].ToString());
        }

        return objValues;
    }

    private static void CheckInvariants(string? propertyName, string? propertyValue)
    {
        propertyName.CheckForNull(() =>
        {
            throw new ArgumentException("The property name passed in for invariant check is invalid.", nameof(propertyName));
        });
        propertyValue.CheckForNull();
    }
}
