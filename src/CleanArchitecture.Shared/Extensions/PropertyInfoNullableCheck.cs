using System.Reflection;

namespace CleanArchitecture.Shared.Extensions;

public static class PropertyInfoNullableCheck
{
    public static bool IsValueTypeNullable(this PropertyInfo property)
    {
        return Nullable.GetUnderlyingType(property.PropertyType) == null;
    }
    
    public static bool IsReferenceTypeNullable(this PropertyInfo property)
    {
        var nullabilityInfo = new NullabilityInfoContext().Create(property);
        return nullabilityInfo.ReadState == NullabilityState.Nullable;
    }
}
