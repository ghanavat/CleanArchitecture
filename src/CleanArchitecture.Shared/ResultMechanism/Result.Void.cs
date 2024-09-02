using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.ResultMechanism;

/// <summary>
/// A different variant of the global Result.
/// Use this when you want to populate/return Result without having to specify its T 'Data' type.
/// </summary>
public class Result : Result<Result>
{
    protected Result(ResultStatus status) : base(status)
    {}

    /// <summary>
    /// Represents invalid result with validation errors.
    /// </summary>
    /// <param name="validationErrors"></param>
    /// <returns></returns>
    public new static Result Invalid(IEnumerable<ValidationError> validationErrors) 
    {  
        return new Result(ResultStatus.Invalid)
        {
            ValidationErrors = validationErrors
        }; 
    }
}
