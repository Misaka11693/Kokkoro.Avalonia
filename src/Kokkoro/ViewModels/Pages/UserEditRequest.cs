using Kokkoro.Models;

namespace Kokkoro.ViewModels.Pages;

/// <summary>打开用户编辑对话框时的上下文。</summary>
public sealed record UserEditRequest(User? Existing, bool IsNew);
