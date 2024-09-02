using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Shared.Extensions;

/// <summary>
/// Implementation of extension as guard clause
/// </summary>
/// <example>
/// someObject.CheckForNull();
/// someobject.CheckforNull("My custom exception message");
/// mediator.CheckNotNull(null, () => { throw new Exception("exception message"); });
/// </example>
public static class GuardClauseExtension
{
    /// <summary>
    /// An extension method to guard against null.
    /// </summary>
    /// <typeparam name="T">The object the null check is done against.</typeparam>
    /// <param name="objectToCheck"></param>
    /// <param name="customMessage">Optional. Custom exception message.</param>
    /// <param name="customException">Optional. A function to create custome exception.</param>
    /// <returns> <paramref name="objectToCheck" /> if its value is not null.</returns>
    public static T CheckNotNull<T>([NotNull] this T? objectToCheck,
        string? customMessage = null,
        Func<Exception>? customException = null)
    {
        if (objectToCheck is not null) return objectToCheck;
        
        var exception = customException?.Invoke();

        if (string.IsNullOrEmpty(customMessage))
        {
            throw exception ?? new ArgumentNullException(nameof(objectToCheck));
        }
        throw exception ?? new ArgumentNullException(nameof(customMessage), customMessage);

    }
}
