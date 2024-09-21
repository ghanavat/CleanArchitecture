﻿using CleanArchitecture.Shared.Enums;

namespace CleanArchitecture.Shared.ResultMechanism;

/// <summary>
/// A different variant of the global Result.
/// Use this when you want to populate/return Result without having to specify its T 'Data' type.
/// </summary>
public class Result : Result<Result>
{
    /// <summary>
    /// Default constructor derived from its base
    /// </summary>
    public Result() : base() { }

    /// <summary>
    /// A constructor that accepts <paramref name="status"/>
    /// </summary>
    /// <param name="status"></param>
    protected Result(ResultStatus status) : base(status) { }

    /// <summary>
    /// Represents a successful operation without return type
    /// </summary>
    /// <returns>A Success Result</returns>
    public static Result Success() => new();

    /// <summary>
    /// Represents invalid result with validation errors.
    /// </summary>
    /// <param name="validationErrors"></param>
    /// <returns>An Invalid Result</returns>
    public new static Result Invalid(IEnumerable<ValidationError> validationErrors)
    {
        return new Result(ResultStatus.Invalid)
        {
            ValidationErrors = validationErrors
        };
    }

    /// <summary>
    /// Represents the error result with the error message
    /// </summary>
    /// <returns>An Error Result</returns>
    public new static Result Error(string errorMessage) => new(ResultStatus.Error)
    {
        ErrorMessages = [errorMessage]
    };
}
