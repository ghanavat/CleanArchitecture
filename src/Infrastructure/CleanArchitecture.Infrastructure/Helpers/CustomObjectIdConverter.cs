using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;

namespace CleanArchitecture.Infrastructure.Helpers;

/// <summary>
/// Custom value converter to convert id members to ObjectId and vice versa
/// </summary>
public class CustomObjectIdConverter : ValueConverter<string, ObjectId>
{
    public CustomObjectIdConverter() 
        : base(id => ObjectId.Parse(id), 
              objectId => objectId.ToString())
    {
    }
}
