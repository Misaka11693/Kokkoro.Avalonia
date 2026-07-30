namespace Kokkoro.Models;

/// <summary>用户列表行数据。</summary>
public sealed class User
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Status { get; set; } = "正常";

    public int Age { get; set; } = 25;

    public DateTime? LastLoginAt { get; set; }

    public bool CanEdit => Age > 40;
}
