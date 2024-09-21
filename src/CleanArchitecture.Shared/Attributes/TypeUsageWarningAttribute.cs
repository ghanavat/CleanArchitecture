namespace CleanArchitecture.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
#pragma warning disable CS1591
public class TypeUsageWarningAttribute : Attribute
{
    public TypeUsageWarningAttribute(Type type)
    {
        //if (type.IsClass) throw new TypeAccessException("Dependency rule violation");

        if (type.IsClass)
        {
            #pragma warning disable CS1030 // #warning directive
            #warning Dependency rule violation.
            #pragma warning restore CS1030 // #warning directive
        }
    }
}
