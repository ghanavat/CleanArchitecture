namespace CleanArchitecture.Api.Requests;

public class CreatePlayerRequestModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Comment { get; set; }
}
