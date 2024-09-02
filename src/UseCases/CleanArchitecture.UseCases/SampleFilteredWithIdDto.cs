namespace CleanArchitecture.UseCases;

/// <summary>
/// A sample DTO object.
/// This DTO holds on to the response data to be returned to the user.
/// I would suggest to use 'record' here as they are immutable
/// <code>public record FeatureOneDto(int id, string Name, string description);</code>
/// </summary>
public class SampleFilteredWithIdDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
