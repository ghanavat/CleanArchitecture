using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.ResultMechanism;

public interface IResult
{
    ResultStatus Status { get; }
    IEnumerable<string> ErrorMessages { get; }
    IEnumerable<ValidationError> ValidationErrors { get; }
}
