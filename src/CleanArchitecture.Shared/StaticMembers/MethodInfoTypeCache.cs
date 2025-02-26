using System.Collections.Concurrent;
using System.Reflection;

namespace CleanArchitecture.Shared.StaticMembers;

/// <summary>
/// A non-generic type to store a static method info cache collection.
/// </summary>
public class MethodInfoTypeCache
{
    /// <summary>
    /// A concurrent dictionary to safely store and cache factory method info.
    /// </summary>
    protected static ConcurrentDictionary<string, MethodInfo> CachedMethodInfoCollection { get; set; } = new();
}
