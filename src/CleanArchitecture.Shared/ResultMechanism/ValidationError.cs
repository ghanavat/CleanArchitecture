using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.ResultMechanism;

public class ValidationError
{
    public ValidationError()
    {}

    public ValidationError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public ValidationError(string errorMessage, string errorCode, ValidationErrorType validationError)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        ValidationErrorType = validationError;
    }

    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }
    public ValidationErrorType ValidationErrorType { get; set; }
}
