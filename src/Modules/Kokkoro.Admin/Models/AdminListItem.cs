namespace Kokkoro.Admin.Models;

public class AdminListItem
{
    public required string Code { get; init; }

    public required string Name { get; init; }

    public required string Department { get; init; }

    public required string Status { get; init; }

    public DateTime UpdatedAt { get; init; }
}
