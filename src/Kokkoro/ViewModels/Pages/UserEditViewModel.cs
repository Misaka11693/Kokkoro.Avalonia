using System.Collections.ObjectModel;
using Kokkoro.Models;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

public partial class UserEditViewModel : ViewModelBase
{
    public UserEditViewModel(UserEditRequest request)
    {
        IsNew = request.IsNew;
        Title = request.IsNew ? "新增用户" : "修改用户";
        CanEditCode = request.IsNew;

        if (request.Existing is { } existing)
        {
            Code = existing.Code;
            Name = existing.Name;
            Department = existing.Department;
            Role = existing.Role;
            Email = existing.Email;
            Status = existing.Status;
            Age = existing.Age;
        }

        _existing = request.Existing;
    }

    private readonly User? _existing;

    public string Title { get; }

    public bool IsNew { get; }

    public bool CanEditCode { get; }

    public ReadOnlyCollection<string> StatusOptions { get; } =
        new(["正常", "停用"]);

    [Reactive]
    private string _code = string.Empty;

    [Reactive]
    private string _name = string.Empty;

    [Reactive]
    private string _department = string.Empty;

    [Reactive]
    private string _role = string.Empty;

    [Reactive]
    private string _email = string.Empty;

    [Reactive]
    private string _status = "正常";

    [Reactive]
    private int _age = 25;

    [Reactive]
    private string _errorMessage = string.Empty;

    [Reactive]
    private bool _hasError;

    public bool TryConfirm(out User? user)
    {
        user = null;
        ClearError();

        var code = Code.Trim();
        var name = Name.Trim();
        var department = Department.Trim();
        var role = Role.Trim();
        var email = Email.Trim();
        var status = Status.Trim();

        if (string.IsNullOrWhiteSpace(code))
        {
            SetError("请填写用户编码。");
            return false;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            SetError("请填写姓名。");
            return false;
        }

        if (string.IsNullOrWhiteSpace(department))
        {
            SetError("请填写部门。");
            return false;
        }

        if (string.IsNullOrWhiteSpace(role))
        {
            SetError("请填写角色。");
            return false;
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            SetError("请填写邮箱。");
            return false;
        }

        if (!email.Contains('@', StringComparison.Ordinal))
        {
            SetError("邮箱格式不正确。");
            return false;
        }

        if (Age is < 1 or > 120)
        {
            SetError("年龄须在 1～120 之间。");
            return false;
        }

        if (IsNew)
        {
            user = new User
            {
                Code = code,
                Name = name,
                Department = department,
                Role = role,
                Email = email,
                Status = string.IsNullOrWhiteSpace(status) ? "正常" : status,
                Age = Age,
                LastLoginAt = null,
            };
            return true;
        }

        if (_existing is null)
        {
            SetError("未找到要修改的用户。");
            return false;
        }

        _existing.Name = name;
        _existing.Department = department;
        _existing.Role = role;
        _existing.Email = email;
        _existing.Status = string.IsNullOrWhiteSpace(status) ? "正常" : status;
        _existing.Age = Age;
        user = _existing;
        return true;
    }

    private void ClearError()
    {
        HasError = false;
        ErrorMessage = string.Empty;
    }

    private void SetError(string message)
    {
        HasError = true;
        ErrorMessage = message;
    }
}
