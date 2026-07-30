using Kokkoro.Core.Models;
using Kokkoro.Services.Users.Dtos;

namespace Kokkoro.Services.Users;

public class UserService : IUserService
{
    private static readonly List<UserDto> Users = CreateSampleUsers();

    public async Task<PageResponse<UserDto>> GetPageAsync(UserQueryDto query)
    {
        await Task.Delay(300);

        var filtered = ApplyFilter(query).ToList();
        var pageIndex = query.PageIndex <= 0 ? 1 : query.PageIndex;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        return new PageResponse<UserDto>
        {
            TotalCount = filtered.Count,
            Items = filtered
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList()
        };
    }

    public Task<bool> ContainsCodeAsync(string code)
    {
        var exists = Users.Any(user => user.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }

    public Task AddAsync(UserDto user)
    {
        Users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(UserDto user)
    {
        var existing = Users.FirstOrDefault(item => item.Code.Equals(user.Code, StringComparison.OrdinalIgnoreCase));
        if (existing is not null)
        {
            existing.Name = user.Name;
            existing.Department = user.Department;
            existing.Role = user.Role;
            existing.Email = user.Email;
            existing.Status = user.Status;
            existing.Age = user.Age;
            existing.LastLoginAt = user.LastLoginAt;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string code)
    {
        var existing = Users.FirstOrDefault(item => item.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        if (existing is not null)
        {
            Users.Remove(existing);
        }

        return Task.CompletedTask;
    }

    private static IEnumerable<UserDto> ApplyFilter(UserQueryDto query)
    {
        IEnumerable<UserDto> items = Users;

        if (!string.IsNullOrWhiteSpace(query.Code))
        {
            var code = query.Code.Trim();
            items = items.Where(user => user.Code.Contains(code, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            var name = query.Name.Trim();
            items = items.Where(user => user.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        return items;
    }

    private static List<UserDto> CreateSampleUsers() =>
    [
        new()
        {
            Code = "U001",
            Name = "张三",
            Department = "研发部",
            Role = "管理员",
            Email = "zhangsan@example.com",
            Status = "正常",
            Age = 28,
            LastLoginAt = new DateTime(2026, 6, 1, 9, 12, 0),
        },
        new()
        {
            Code = "U002",
            Name = "李四",
            Department = "研发部",
            Role = "开发",
            Email = "lisi@example.com",
            Status = "正常",
            Age = 35,
            LastLoginAt = new DateTime(2026, 6, 2, 14, 35, 0),
        },
        new()
        {
            Code = "U003",
            Name = "王五",
            Department = "产品部",
            Role = "产品经理",
            Email = "wangwu@example.com",
            Status = "正常",
            Age = 42,
            LastLoginAt = new DateTime(2026, 5, 28, 18, 0, 0),
        },
        new()
        {
            Code = "U004",
            Name = "赵六",
            Department = "运营部",
            Role = "运营",
            Email = "zhaoliu@example.com",
            Status = "停用",
            Age = 55,
            LastLoginAt = null,
        },
        new()
        {
            Code = "U005",
            Name = "钱七",
            Department = "财务部",
            Role = "财务",
            Email = "qianqi@example.com",
            Status = "正常",
            Age = 23,
            LastLoginAt = new DateTime(2026, 6, 2, 8, 45, 0),
        },
        new()
        {
            Code = "U006",
            Name = "孙八",
            Department = "人事部",
            Role = "HR",
            Email = "sunba@example.com",
            Status = "正常",
            Age = 31,
            LastLoginAt = new DateTime(2026, 5, 30, 11, 20, 0),
        },
        new()
        {
            Code = "U007",
            Name = "周九",
            Department = "研发部",
            Role = "开发",
            Email = "zhoujiu@example.com",
            Status = "正常",
            Age = 26,
            LastLoginAt = new DateTime(2026, 6, 1, 16, 0, 0),
        },
        new()
        {
            Code = "U008",
            Name = "吴十",
            Department = "产品部",
            Role = "产品",
            Email = "wushi@example.com",
            Status = "正常",
            Age = 48,
            LastLoginAt = new DateTime(2026, 5, 29, 10, 15, 0),
        },
        new()
        {
            Code = "U009",
            Name = "郑十一",
            Department = "运营部",
            Role = "运营",
            Email = "zheng11@example.com",
            Status = "正常",
            Age = 17,
            LastLoginAt = new DateTime(2026, 6, 2, 9, 30, 0),
        },
        new()
        {
            Code = "U010",
            Name = "王十二",
            Department = "财务部",
            Role = "财务",
            Email = "wang12@example.com",
            Status = "正常",
            Age = 52,
            LastLoginAt = new DateTime(2026, 5, 31, 14, 0, 0),
        },
        new()
        {
            Code = "U011",
            Name = "冯十三",
            Department = "人事部",
            Role = "HR",
            Email = "feng13@example.com",
            Status = "停用",
            Age = 63,
            LastLoginAt = null,
        },
        new()
        {
            Code = "U012",
            Name = "陈十四",
            Department = "研发部",
            Role = "测试",
            Email = "chen14@example.com",
            Status = "正常",
            Age = 22,
            LastLoginAt = new DateTime(2026, 6, 2, 11, 45, 0),
        },
        new()
        {
            Code = "U013",
            Name = "褚十五",
            Department = "研发部",
            Role = "开发",
            Email = "chu15@example.com",
            Status = "正常",
            Age = 39,
            LastLoginAt = new DateTime(2026, 6, 1, 8, 20, 0),
        },
        new()
        {
            Code = "U014",
            Name = "卫十六",
            Department = "产品部",
            Role = "设计",
            Email = "wei16@example.com",
            Status = "正常",
            Age = 45,
            LastLoginAt = new DateTime(2026, 5, 27, 17, 50, 0),
        },
        new()
        {
            Code = "U015",
            Name = "蒋十七",
            Department = "运营部",
            Role = "运营",
            Email = "jiang17@example.com",
            Status = "正常",
            Age = 68,
            LastLoginAt = new DateTime(2026, 6, 2, 13, 10, 0),
        },
    ];
}
