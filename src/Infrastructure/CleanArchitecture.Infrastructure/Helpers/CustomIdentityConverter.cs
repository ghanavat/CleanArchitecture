using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;

namespace CleanArchitecture.Infrastructure.Helpers;

/// <summary>
/// Custom value converter to convert id members to ObjectId and vice versa
/// </summary>
public class CustomIdentityConverter : ValueConverter<string?, ObjectId>
{
    public CustomIdentityConverter() 
        : base(value => ObjectId.Parse(value), 
              value => value.ToString())
    {
    }
}
