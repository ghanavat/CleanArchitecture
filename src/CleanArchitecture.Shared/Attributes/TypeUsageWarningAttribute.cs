using System.ComponentModel;

namespace CleanArchitecture.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class TypeUsageWarningAttribute : Attribute
{
    public TypeUsageWarningAttribute(Type type)
    {
        if (type.IsClass) throw new WarningException("Dependency rule violation");
    }
}
