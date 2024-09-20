namespace SampleApi.Requests;

/// <summary>
/// Create new player request model
/// </summary>
public class CreatePlayerRequestModel
{
    /// <summary>
    /// First name property
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Last name property
    /// </summary>
    public required string Lastname { get; set; }
}
