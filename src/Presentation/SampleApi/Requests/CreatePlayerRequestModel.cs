namespace SampleApi.Requests;

/// <summary>
/// Create a new player request model
/// </summary>
#pragma warning disable CS1591
public class CreatePlayerRequestModel
{
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
}
