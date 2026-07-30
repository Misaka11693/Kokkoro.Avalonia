namespace Kokkoro.Models;

/// <summary>角色列表行数据。</summary>
public sealed class Role
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int MemberCount { get; set; }

    public string Status { get; set; } = "启用";

    public string Description { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; }
}
