namespace Kokkoro.Admin.Models;

public sealed class AdminTreeNode
{
    public required string Name { get; init; }

    public required string Type { get; init; }

    public string? Description { get; init; }

    public IReadOnlyList<AdminTreeNode> Children { get; init; } = [];
}
