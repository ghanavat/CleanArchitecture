namespace CleanArchitecture.Shared;

/// <summary>
/// Value Object logic base class
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// ...
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// To implement equality instead of identity, which is the default.
    /// Why did I override this? By default, DotNet uses Reference Equality when comparing objects.
    /// However, for Value Objects, two instances should be considered equal if all of their properties/attributes are equal,
    /// not just because theu point to the same memory location.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    /// Objects that are considered equal, for Value Objects, they must also have the same hash code.
    /// Hash codes are used in collections.
    /// I've overriddeen GetHashCode to generate new hash code based on the value of the attributes (obj).
    /// </summary>
    /// <returns></returns>

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }
}
