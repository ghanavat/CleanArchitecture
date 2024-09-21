using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.Attributes;

/// <summary>
/// To mark a factory method.
/// </summary>
/// <remarks>
/// This is used by the base factory to look up the factory method with some validation
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class FactoryMethodAttribute : Attribute
{
    /// <summary>
    /// Enum parameter that is used to mark the factory method with its parent class name
    /// </summary>
    public FactoryMethodFor FactoryMethodName { get; }

    /// <summary>
    /// The custom attribute constructor
    /// </summary>
    /// <param name="factoryMethodName">Am enum to indicate what aggregate root this attribute belongs to. 
    /// This is used to look up the factory method in the aggregate root class</param>
    public FactoryMethodAttribute(FactoryMethodFor factoryMethodName)
    {
        if (!factoryMethodName.Equals(FactoryMethodFor.None))
        {
            FactoryMethodName = factoryMethodName;
        }
    }
}
