using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Kokkoro.Services.Users.Dtos;

namespace Kokkoro.Models;

/// <summary>用户列表查询条件。</summary>
public partial class UserQueryCriteria : ReactiveObject
{
    [Reactive]
    private string _code = string.Empty;

    [Reactive]
    private string _name = string.Empty;

    public void Reset()
    {
        Code = string.Empty;
        Name = string.Empty;
    }

    public UserQueryDto ToDto(int currentPage, int pageSize) => new()
    {
        Code = string.IsNullOrWhiteSpace(Code) ? null : Code.Trim(),
        Name = string.IsNullOrWhiteSpace(Name) ? null : Name.Trim(),
        PageIndex = currentPage,
        PageSize = pageSize,
    };
}
