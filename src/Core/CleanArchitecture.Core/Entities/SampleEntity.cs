using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.Enums;
using CleanArchitecture.Template.Shared.Attributes;

namespace CleanArchitecture.Core.Entities;

/// <summary>
/// A sample Entity class.
/// This is where you put the business rules. Things like add new item or update/delete.
/// </summary>
public class SampleEntity : EntityBase, IAggregateRoot
{
    public SampleEntity() { }

    private SampleEntity(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public string? FirstName { get; set; }
    public string? LastName { get; private set; }

    [FactoryMethod(FactoryMethodFor.SampleEntity)]
    public SampleEntity AddSampleEntityItem(string id, string firstName, string lastName)
    {
        var sampleEntity = new SampleEntity(id, firstName, lastName);
        // Construct any value object or FK references and other aggregates if necessary

        return sampleEntity;
    }

    public void UpdateName(string name)
    {
        // Don't forget guard clause
        FirstName = name;
    }
}
