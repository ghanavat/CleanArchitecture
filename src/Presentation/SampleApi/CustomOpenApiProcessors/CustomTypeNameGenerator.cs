using NJsonSchema;

namespace SampleApi.CustomOpenApiProcessors;

/// <summary>
/// Custom Type Name Generator to apply customised behaviour on type names
/// </summary>
public class CustomTypeNameGenerator : ITypeNameGenerator
{
    /// <summary>
    /// Interface implementation.
    /// It is implemented to skip any changes in the schema names during the types generation
    /// </summary>
    /// <param name="schema">The schema object that the operation is based on</param>
    /// <param name="typeNameHint">The type name that we want to customise</param>
    /// <param name="reservedTypeNames">List of the reserved type names. Values will be actual type names without changes.</param>
    /// <returns>String value of Type Name that was skipped</returns>
    public string Generate(JsonSchema schema, string? typeNameHint, IEnumerable<string> reservedTypeNames)
    {
        if (string.IsNullOrEmpty(typeNameHint) && !string.IsNullOrEmpty(schema.DocumentPath))
        {
            typeNameHint = schema.DocumentPath.Replace("\\", "/").Split('/').Last();
        }

        return typeNameHint!;
    }
}
