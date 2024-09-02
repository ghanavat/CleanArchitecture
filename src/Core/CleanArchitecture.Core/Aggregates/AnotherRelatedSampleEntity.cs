namespace CleanArchitecture.Core.Aggregates;

public class AnotherRelatedSampleEntity
{
    // This property is like a primary key. Consider EntityBase class for it.
    public int Id { get; set; }
    public int? AnotherSampleEntityId { get; set; }
    public required string SomeProperty { get; set; }

    // We are adding a reference of AnotherSampleEntity to this entity
    public void AddAnotherSampleEntity(int anotherSampleEntityId)
    {
        // Don't forget guard clause
        AnotherSampleEntityId = anotherSampleEntityId;
    }

    public void UpdateSomeProperty(string updateSomeProperty)
    {
        // Don't forget guard clause
        SomeProperty = updateSomeProperty;
    }

    /// <summary>
    /// This is an example domain operation as a method to delete the reference from this entity to AnotherSampleEntity
    /// </summary>
    public void DeleteAnotherSampleEntity()
    {
        AnotherSampleEntityId = null;
    }
}
