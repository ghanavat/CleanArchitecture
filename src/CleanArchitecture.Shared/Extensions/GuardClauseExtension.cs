using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CleanArchitecture.Shared.Extensions;

/// <summary>
/// Guard clause extension.
/// </summary>
public static class GuardClauseExtension
{
    private const string StandardArgumentNullMessage = "Argument cannot be null or empty.";

    /// <summary>
    /// An extension method to guard against null for class objects.
    /// </summary>
    /// <typeparam name="T">The object the null check is done against.</typeparam>
    /// <param name="input">The generic object that is being checked for its state.</param>
    /// <param name="paramName"></param>
    /// <param name="customException">Optional. A function to create custom exception.</param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    /// <example>
    /// <code>
    /// someObject.CheckForNull();
    /// someObject.CheckForNull(() => { throw new MyCustomException("custom exception message"); });
    /// </code>
    /// </example>
    public static T CheckForNull<T>([NotNull] this T? input,
        Func<Exception>? customException = null,
        [CallerArgumentExpression("input")] string? paramName = null) where T : class
    {
        var exception = customException?.Invoke();

        if (input is null)
        {
            throw exception ?? new ArgumentNullException(paramName, StandardArgumentNullMessage);
        }

        return input;
    }
    
    /// <summary>
    /// Check if the collection is null or empty
    /// </summary>
    /// <param name="inputCollection"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A boolean result of the check</returns>
    public static bool IsNullOrAny<T>([NotNullWhen(false)] this IEnumerable<T>? inputCollection) where T : class
    {
        return inputCollection is not null && inputCollection.Any();
    }
}
