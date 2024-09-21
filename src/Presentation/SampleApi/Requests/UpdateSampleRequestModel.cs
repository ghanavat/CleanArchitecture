namespace SampleApi.Requests;

/// <summary>
/// Request Model
/// </summary>
#pragma warning disable CS1591
public class UpdateSampleRequestModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
