using System.Diagnostics.CodeAnalysis;

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
    /// <param name="customException">Optional. A function to create custome exception.</param>
    /// <returns><paramref name="input" /> if the value is not null.</returns>
    /// <example>
    /// <code>
    /// someObject.CheckForNull();
    /// someObject.CheckForNull(() => { throw new MyCustomException("custom exception message"); });
    /// </code>
    /// </example>
    public static T CheckForNull<T>([NotNull] this T? input,
        Func<Exception>? customException = null) where T : class
    {
        Exception? exception = customException?.Invoke();

        if (input!.GetType().Name.Equals(nameof(String))
            && string.IsNullOrEmpty(input.ToString()))
        {
            throw exception ?? new ArgumentNullException(nameof(input), StandardArgumentNullMessage);
        }
        else if (input is null)
        {
            throw exception ?? new ArgumentNullException(nameof(input), StandardArgumentNullMessage);
        }

        return input;
    }
}
