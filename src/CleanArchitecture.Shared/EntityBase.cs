namespace CleanArchitecture.Shared;

/// <summary>
/// Abstract entity base.
/// </summary>
# pragma warning disable CS1591
public abstract class EntityBase
{
    /// <summary>
    /// We are assuming that the ID type is string. ObjectId in MongoDb and string in DotNet.
    /// </summary>
    public virtual string? Id { get; set; }

    protected EntityBase()
    { }

    protected EntityBase(string id)
    {
        Id = id;
    }
}
