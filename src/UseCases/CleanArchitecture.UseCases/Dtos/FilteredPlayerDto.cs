namespace CleanArchitecture.UseCases.Dtos;

public class FilteredPlayerDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }

    public bool IsDeleted { get; set; }
}
