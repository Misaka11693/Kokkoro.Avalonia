using Kokkoro.Core.Models;
using Kokkoro.Services.Roles.Dtos;

namespace Kokkoro.Services.Roles;

public interface IRoleService
{
    Task<PageResponse<RoleDto>> GetPageAsync(RoleQueryDto query);
}
