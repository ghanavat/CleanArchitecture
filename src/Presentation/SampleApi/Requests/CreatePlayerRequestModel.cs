namespace SampleApi.Requests;

public class CreatePlayerRequestModel
{
    public required string FirstName { get; set; }
    public required string Lastname { get; set; }
    public required string Comment { get; set; }
}
