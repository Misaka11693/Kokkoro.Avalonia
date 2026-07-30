using Kokkoro.Core.Models;
using Kokkoro.Services.Roles.Dtos;

namespace Kokkoro.Services.Roles;

public class RoleService : IRoleService
{
    private static readonly List<RoleDto> Roles = CreateSampleRoles();

    public async Task<PageResponse<RoleDto>> GetPageAsync(RoleQueryDto query)
    {
        await Task.Delay(1300);

        var filtered = ApplyFilter(query).ToList();
        var pageIndex = query.PageIndex <= 0 ? 1 : query.PageIndex;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        return new PageResponse<RoleDto>
        {
            TotalCount = filtered.Count,
            Items = filtered
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList()
        };
    }

    private static IEnumerable<RoleDto> ApplyFilter(RoleQueryDto query)
    {
        IEnumerable<RoleDto> items = Roles;

        if (!string.IsNullOrWhiteSpace(query.Code))
        {
            var code = query.Code.Trim();
            items = items.Where(role => role.Code.Contains(code, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            var name = query.Name.Trim();
            items = items.Where(role => role.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Status) && !string.Equals(query.Status, "全部", StringComparison.OrdinalIgnoreCase))
        {
            var status = query.Status.Trim();
            items = items.Where(role => role.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        return items;
    }

    private static List<RoleDto> CreateSampleRoles() =>
    [
        new()
        {
            Code = "R001",
            Name = "系统管理员",
            Category = "系统",
            MemberCount = 2,
            Status = "启用",
            Description = "拥有系统全部菜单与操作权限。",
            UpdatedAt = new DateTime(2026, 6, 20, 9, 30, 0),
        },
        new()
        {
            Code = "R002",
            Name = "研发负责人",
            Category = "业务",
            MemberCount = 3,
            Status = "启用",
            Description = "负责研发团队日常协作与审批。",
            UpdatedAt = new DateTime(2026, 6, 19, 15, 10, 0),
        },
        new()
        {
            Code = "R003",
            Name = "开发工程师",
            Category = "业务",
            MemberCount = 12,
            Status = "启用",
            Description = "参与需求开发、联调与缺陷修复。",
            UpdatedAt = new DateTime(2026, 6, 18, 10, 0, 0),
        },
        new()
        {
            Code = "R004",
            Name = "测试工程师",
            Category = "业务",
            MemberCount = 5,
            Status = "启用",
            Description = "负责测试计划、用例和回归验证。",
            UpdatedAt = new DateTime(2026, 6, 17, 13, 45, 0),
        },
        new()
        {
            Code = "R005",
            Name = "产品经理",
            Category = "业务",
            MemberCount = 4,
            Status = "启用",
            Description = "负责产品规划、需求分析与迭代管理。",
            UpdatedAt = new DateTime(2026, 6, 16, 17, 20, 0),
        },
        new()
        {
            Code = "R006",
            Name = "访客",
            Category = "系统",
            MemberCount = 8,
            Status = "停用",
            Description = "仅保留查看权限，当前已停用。",
            UpdatedAt = new DateTime(2026, 6, 15, 11, 5, 0),
        },
        new()
        {
            Code = "R007",
            Name = "运营专员",
            Category = "业务",
            MemberCount = 6,
            Status = "启用",
            Description = "负责活动配置、投放与数据跟踪。",
            UpdatedAt = new DateTime(2026, 6, 14, 8, 40, 0),
        },
        new()
        {
            Code = "R008",
            Name = "财务审核",
            Category = "审批",
            MemberCount = 2,
            Status = "启用",
            Description = "负责付款、报销等财务流程审核。",
            UpdatedAt = new DateTime(2026, 6, 13, 16, 25, 0),
        },
    ];
}
