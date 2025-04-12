using Ghanavats.Domain.Primitives;

namespace CleanArchitecture.Core.GameAggregate.ValueObjects;

internal class SampleValueObject : ValueObject
{
    public string SomeProperty { get; set; }

    /// <summary>
    /// Sample value object constructor
    /// </summary>
    /// <param name="someProperty"></param>
    public SampleValueObject(string someProperty)
    {
        SomeProperty = someProperty;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SomeProperty;
    }
}
