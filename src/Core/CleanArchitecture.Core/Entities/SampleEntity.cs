using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Core.Entities;

/// <summary>
/// A sample Entity class.
/// This is where you put the business rules. Things like add new item or update/delete.
/// </summary>
public class SampleEntity : IIdentifiable
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }

    /// <summary>
    /// Bad idea
    /// </summary>
    public void AddSampleEntityItem()
    {
        /* 
         * For documentation/intention purposes: 
         * This is not supposed to be here. 
         * When an entity needs to be populated for the first time, it is not a domain operation. 
         * Instead, it needs to be dealt with at the application layer.
        */
    }

    public void UpdateName(string name)
    {
        // Don't forget guard clause
        FirstName = name;
    }
}
