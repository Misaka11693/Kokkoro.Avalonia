using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Session;

public partial class CurrentUserViewModel : ViewModelBase
{
    [Reactive]
    private string _displayName = "未登录";

    [Reactive]
    private string _email = string.Empty;

    [Reactive]
    private string _avatarText = "KO";

    public void SignIn(string userName)
    {
        var normalizedName = string.IsNullOrWhiteSpace(userName) ? "Kokkoro" : userName.Trim();

        DisplayName = normalizedName;
        Email = $"{normalizedName}@kokkoro.local";
        AvatarText = normalizedName.Length >= 2
            ? normalizedName[..2].ToUpperInvariant()
            : normalizedName.ToUpperInvariant();
    }

    public void SignOut()
    {
        DisplayName = "未登录";
        Email = string.Empty;
        AvatarText = "KO";
    }
}
