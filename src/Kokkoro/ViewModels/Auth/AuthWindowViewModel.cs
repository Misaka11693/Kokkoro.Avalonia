using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;
using Kokkoro.ViewModels.Core;
using Kokkoro.ViewModels.Session;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Auth;

public partial class AuthWindowViewModel : ViewModelBase
{
    private const double PanelWidthValue = 420d;
    private const double FormWidthValue = 336d;
    private const double FormSpacingValue = 32d;
    private const double FormSlideOffset = FormWidthValue + FormSpacingValue;
    private const double SignInHeight = 364d;
    private const double SignInErrorHeight = 438d;
    private const double SignUpHeight = 492d;
    private const double SignUpErrorHeight = 540d;

    private readonly CurrentUserViewModel _currentUser;

    public AuthWindowViewModel(CurrentUserViewModel currentUser)
    {
        _currentUser = currentUser;
    }

    [Reactive]
    private bool _isSignUpMode;

    [Reactive]
    private string _account = string.Empty;

    [Reactive]
    private string _password = string.Empty;

    [Reactive]
    private string _signUpUserName = string.Empty;

    [Reactive]
    private string _signUpEmail = string.Empty;

    [Reactive]
    private string _signUpPassword = string.Empty;

    [Reactive]
    private string _confirmPassword = string.Empty;

    [Reactive]
    private string _errorMessage = string.Empty;

    [Reactive]
    private bool _hasError;

    public event EventHandler? SignInSucceeded;

    public double SignInBackdropOpacity => IsSignUpMode ? 0d : 1d;

    public double SignUpBackdropOpacity => IsSignUpMode ? 1d : 0d;

    public double FormSlideOffsetX => IsSignUpMode ? -FormSlideOffset : 0d;

    public double PanelWidth => PanelWidthValue;

    public double PanelHeight => IsSignUpMode
        ? (HasError ? SignUpErrorHeight : SignUpHeight)
        : (HasError ? SignInErrorHeight : SignInHeight);

    public double FormWidth => FormWidthValue;

    public double FormTrackWidth => (FormWidthValue * 2d) + FormSpacingValue;

    public double FormSpacing => FormSpacingValue;

    [ReactiveCommand]
    private void ToggleMode()
    {
        IsSignUpMode = !IsSignUpMode;
        ClearError();
        RaiseLayoutChanged();
    }

    [ReactiveCommand]
    private void Submit()
    {
        if (IsSignUpMode)
        {
            SignUp();
            return;
        }
        SignIn();
    }

    private void SignIn()
    {
        if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password))
        {
            SetError("Please enter your account and password.");
            return;
        }

        if (!string.Equals(Password, "111111", StringComparison.Ordinal))
        {
            SetError("Invalid password. Use 111111 for the demo.");
            return;
        }

        ClearError();
        _currentUser.SignIn(Account);
        SignInSucceeded?.Invoke(this, EventArgs.Empty);
    }

    private void SignUp()
    {
        if (string.IsNullOrWhiteSpace(SignUpUserName)
            || string.IsNullOrWhiteSpace(SignUpEmail)
            || string.IsNullOrWhiteSpace(SignUpPassword)
            || string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            SetError("Please complete all sign-up fields.");
            return;
        }

        if (!SignUpEmail.Contains("@", StringComparison.Ordinal))
        {
            SetError("Please enter a valid email address.");
            return;
        }

        if (!string.Equals(SignUpPassword, ConfirmPassword, StringComparison.Ordinal))
        {
            SetError("The passwords do not match.");
            return;
        }

        if (!string.Equals(SignUpPassword, "111111", StringComparison.Ordinal))
        {
            SetError("Demo sign-up password must be 111111.");
            return;
        }

        Account = SignUpUserName;
        Password = SignUpPassword;
        ClearError();
        ToggleMode();
    }

    private void SetError(string message)
    {
        ErrorMessage = message;
        HasError = true;
        this.RaisePropertyChanged(nameof(PanelHeight));
    }

    private void ClearError()
    {
        ErrorMessage = string.Empty;
        HasError = false;
        this.RaisePropertyChanged(nameof(PanelHeight));
    }

    private void RaiseLayoutChanged()
    {
        this.RaisePropertyChanged(nameof(SignInBackdropOpacity));
        this.RaisePropertyChanged(nameof(SignUpBackdropOpacity));
        this.RaisePropertyChanged(nameof(FormSlideOffsetX));
        this.RaisePropertyChanged(nameof(PanelHeight));
    }
}
