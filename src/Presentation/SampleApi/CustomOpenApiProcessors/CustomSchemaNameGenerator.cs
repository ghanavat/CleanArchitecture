using NJsonSchema.Generation;

namespace SampleApi.CustomOpenApiProcessors;

/// <summary>
/// Custom Schema Name Generator to apply customised behaviour on how generic type names are generated
/// </summary>
public class CustomSchemaNameGenerator : ISchemaNameGenerator
{
    /// <summary>
    /// Interface implementation to apply custom behaviour on schema names
    /// </summary>
    /// <param name="type">The type that needs re-generated/re-adjusted</param>
    /// <returns>String value of Type Name</returns>
    public string Generate(Type type)
    {
        return GenerateTypeName(type);
    }

    /// <summary>
    /// Generates human-readable schema names for generic types.
    /// </summary>
    /// <param name="type">Type to generate new schema name for</param>
    /// <returns>String of the new schema name</returns>
    private static string GenerateTypeName(Type type)
    {
        var typeName = type.Name;

        if (!type.IsGenericType) return typeName;
        
        /* Retrieving the generic argument of the 'type',
             * i.e. 'SampleDto' of 'Result<SampleDto>'
        */
        var genericArgument = string.Join(",", type.GetGenericArguments().Select(x => x.Name));

        /* Generic types in DotNet are named with '`' suffix.
             * We are retrieving the index of the '`' character.
             */
        var index = typeName.IndexOf('`');

        /* Removing the '`' character by its index and creating a new string object.
             * I.e., 'Result`' will be 'Result'
             */
        var typeNameWithoutGeneric = index == -1 ? typeName : typeName[..index];

        return $"{typeNameWithoutGeneric}<{genericArgument}>";
    }
}
