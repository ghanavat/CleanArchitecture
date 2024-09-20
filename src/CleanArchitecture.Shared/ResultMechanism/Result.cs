using System.Text.Json.Serialization;
using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.ResultMechanism;

public class Result<T>
{
    protected Result(T data)
    {
        Data = data;
    }

    protected Result(T data, string successMessage) : this(data)
    {
        SuccessMessage = successMessage;
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    [JsonIgnore]
    public bool IsSuccess => Status is ResultStatus.Ok;

    [JsonInclude]
    public T? Data { get; set; }

    /// <summary>
    /// Set is protected and accessible by derived classes
    /// </summary>
    [JsonInclude]
    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    /// <summary>
    /// Set is protected and accessible by derived classes
    /// </summary>
    [JsonInclude]
    public IEnumerable<string> ErrorMessages { get; protected set; } = [];

    /// <summary>
    /// Set is protected and accessible by derived classes
    /// </summary>
    [JsonInclude]    
    public IEnumerable<ValidationError> ValidationErrors { get; protected set; } = [];

    /// <summary>
    /// Set is protected and accessible by derived classes
    /// </summary>
    [JsonInclude]
    public string SuccessMessage { get; protected set; } = string.Empty;

    /// <summary>
    /// Successful operation with a value as a result
    /// </summary>
    /// <param name="data">Data parameter for setting value to it</param>
    /// <returns>A Result object of <typeparamref name="T"/> </returns>
    public static Result<T> Success(T data)
    {
        /* Default status is OK */
        return new Result<T>(data);
    }

    /// <summary>
    /// Successful operation with a value as a result and a custom message.
    /// </summary>
    /// <param name="data">Data parameter for setting value to it</param>
    /// <param name="successMessage">A custom success message</param>
    /// <returns>A Result object of <typeparamref name="T"/> </returns>
    public static Result<T> Success(T data, string successMessage)
    {
        /* Default status is OK */
        return new Result<T>(data, successMessage);
    }

    /// <summary>
    /// Represents a situation where an error occurred.
    /// </summary>
    /// <param name="errorMessage">A custom error message</param>
    /// <returns>A Result object of <typeparamref name="T"/></returns>
    public static Result<T> Error(string errorMessage)
    {
        return new Result<T>(ResultStatus.Error) { ErrorMessages = new[] { errorMessage } };
    }

    /// <summary>
    /// Represents a situation where the resource is not found.
    /// </summary>
    /// <returns>A Result object of <typeparamref name="T"/></returns>
    public static Result<T> NotFound()
    {
        return new Result<T>(ResultStatus.NotFound);
    }

    /// <summary>
    /// Represents invalid result with validation errors
    /// </summary>
    /// <param name="validationErrors">A list of validation errors</param>
    /// <returns>A Result object of <paramref name="T"/></returns>
    public static Result<T> Invalid(IEnumerable<ValidationError> validationErrors)
    {
        return new Result<T>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
    }

    /// <summary>
    /// An operator to automatically convert the return type within a method to the type being returned
    /// </summary>
    /// <param name="data">The return data</param>
    public static implicit operator Result<T>(T? data) => new(data!);
    public static implicit operator T(Result<T> result) => result.Data!;
    public static implicit operator Result<T>(Result result) => new(default(T)!)
    {
        Status =  result.Status,
        ErrorMessages = result.ErrorMessages,
        SuccessMessage = result.SuccessMessage,
        ValidationErrors = result.ValidationErrors
    };
}
