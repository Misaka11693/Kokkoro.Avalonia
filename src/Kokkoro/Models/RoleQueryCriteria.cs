using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Kokkoro.Services.Roles.Dtos;

namespace Kokkoro.Models;

/// <summary>角色列表查询条件。</summary>
public partial class RoleQueryCriteria : ReactiveObject
{
    [Reactive]
    private string _code = string.Empty;

    [Reactive]
    private string _name = string.Empty;

    [Reactive]
    private string _status = "全部";

    public void Reset()
    {
        Code = string.Empty;
        Name = string.Empty;
        Status = "全部";
    }

    public RoleQueryDto ToDto(int currentPage, int pageSize) => new()
    {
        Code = string.IsNullOrWhiteSpace(Code) ? null : Code.Trim(),
        Name = string.IsNullOrWhiteSpace(Name) ? null : Name.Trim(),
        Status = string.IsNullOrWhiteSpace(Status) ? null : Status.Trim(),
        PageIndex = currentPage,
        PageSize = pageSize,
    };
}
