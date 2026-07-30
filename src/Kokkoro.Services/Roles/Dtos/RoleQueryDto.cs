namespace Kokkoro.Services.Roles.Dtos;

public class RoleQueryDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}
