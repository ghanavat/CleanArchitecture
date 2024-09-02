namespace SampleApi.Requests;

/// <summary>
/// Request Model
/// </summary>
public class UpdateSampleRequestModel
{
    /// <summary>
    /// Ip property which can be used to lookup for a record that is to be updated
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name property which may have new value
    /// </summary>
    public required string Name { get; set; }
}
