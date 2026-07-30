using System;
using Kokkoro.ViewModels.Core;
using Kokkoro.ViewModels.Session;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Main;

public sealed partial class UserAccountButtonViewModel : ViewModelBase
{
    public UserAccountButtonViewModel(CurrentUserViewModel currentUser)
    {
        CurrentUser = currentUser;
    }

    public CurrentUserViewModel CurrentUser { get; }

    public event EventHandler? SignOutRequested;

    [ReactiveCommand]
    private void SignOut()
    {
        SignOutRequested?.Invoke(this, EventArgs.Empty);
    }
}
