using System.Collections.Immutable;

namespace CleanArchitecture.Core.ActionOptions;

public class DomainFactoryOption
{
    private readonly List<string> _ignorePropertyItems = [];
    private readonly Dictionary<string, object> _additionalPropertyItems = [];
    
    public IImmutableList<string> PropertyInfoItems => _ignorePropertyItems.ToImmutableList();
    public IImmutableDictionary<string, object> AdditionalProperties => _additionalPropertyItems.ToImmutableDictionary();
    
    /// <summary>
    /// Properties that are not supposed to be passed to the factory for the creation of the domain entity.
    /// <remarks>Often, commands are sent with all properties to satisfy the business logic.
    /// However, not all of them are needed to create a new domain entity object.</remarks>
    /// </summary>
    /// <param name="propertyNames">A list of property names to be ignored by the factory.</param>
    public void IgnoreProperties(IImmutableList<string> propertyNames)
    {
        foreach (var item in propertyNames)
        {
            _ignorePropertyItems.Add(item);
        }
    }
    
    /// <summary>
    /// Properties that are not sent via the request command and are calculated after the request was sent by clients
    /// </summary>
    /// <remarks>To keep the API requests clean from unnecessary constraints to the business logics,
    /// often some of the domain entity arguments might be calculated in the business logic code,
    /// and then passed to the entity constructor. 
    /// </remarks>
    /// <param name="propertyDetails"></param>
    public void AddProperties(IImmutableDictionary<string, object> propertyDetails)
    {
        foreach (var item in propertyDetails)
        {
            _additionalPropertyItems.Add(item.Key, item.Value);
        }
    }
}
