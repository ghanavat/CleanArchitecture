namespace SampleApi.Requests;

public class CreatePlayerRequestModel
{
    public string FirstName { get; set; } = string.Empty;
    public string? Lastname { get; set; }
    public string? Comment { get; set; }
}
