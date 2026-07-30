using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using Kokkoro.ViewModels.Auth;
using System.ComponentModel;
using Ursa.ReactiveUIExtension;

namespace Kokkoro.Views.Auth;

public partial class AuthWindow : ReactiveUrsaWindow<AuthWindowViewModel>
{
    private const double SyncTolerance = 0.1d;
    private const string SignInBackgroundAsset = "signin-background";
    private const string SignUpBackgroundAsset = "signup-background";
    private static readonly string[] BackgroundExtensions = ["png", "jpg"];
    private readonly TranslateTransform _signInFrostTransform = new();
    private readonly TranslateTransform _signUpFrostTransform = new();
    private bool _syncPending;

    public AuthWindow()
    {
        InitializeComponent();
        SignInFrostImage.RenderTransform = _signInFrostTransform;
        SignUpFrostImage.RenderTransform = _signUpFrostTransform;
        ApplyBackgroundImages();
        AttachGeometryHandlers();
        Opened += OnOpened;
        Closed += OnClosed;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        AttachViewModelHandlers();
        RequestFrostSync();
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        DetachGeometryHandlers();
        DetachViewModelHandlers();
    }

    private void OnSignInSucceeded(object? sender, EventArgs e)
    {
        if (Application.Current is App app)
        {
            app.ShowMainWindow(this);
        }
    }

    private void OnGeometryChanged(object? sender, SizeChangedEventArgs e)
    {
        RequestFrostSync();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (RequiresFrostSync(e.PropertyName))
        {
            RequestFrostSync();
        }
    }

    private void RequestFrostSync()
    {
        if (_syncPending)
        {
            return;
        }

        _syncPending = true;
        Dispatcher.UIThread.Post(SyncFrostImagesOnRender, DispatcherPriority.Render);
    }

    private void SyncFrostImagesOnRender()
    {
        _syncPending = false;
        SyncFrostImages();
    }

    private void SyncFrostImages()
    {
        SyncFrostImage(SignInBackgroundImage, SignInFrostImage, _signInFrostTransform);
        SyncFrostImage(SignUpBackgroundImage, SignUpFrostImage, _signUpFrostTransform);
    }

    private void ApplyBackgroundImages()
    {
        var signInBitmap = LoadBitmap(SignInBackgroundAsset);
        SignInBackgroundImage.Source = signInBitmap;
        SignInFrostImage.Source = signInBitmap;

        var signUpBitmap = LoadBitmap(SignUpBackgroundAsset);
        SignUpBackgroundImage.Source = signUpBitmap;
        SignUpFrostImage.Source = signUpBitmap;
    }

    private void SyncFrostImage(Image backdropImage, Image frostImage, TranslateTransform transform)
    {
        if (backdropImage.Source is null || frostImage.Source is null)
        {
            return;
        }

        var backdropTopLeft = backdropImage.TranslatePoint(new Point(0, 0), AuthWindowRoot);
        var panelTopLeft = AuthPanel.TranslatePoint(new Point(0, 0), AuthWindowRoot);

        if (backdropTopLeft is null || panelTopLeft is null || backdropImage.Bounds.Width <= 0 || backdropImage.Bounds.Height <= 0)
        {
            return;
        }

        SetIfChanged(frostImage.Width, backdropImage.Bounds.Width, width => frostImage.Width = width);
        SetIfChanged(frostImage.Height, backdropImage.Bounds.Height, height => frostImage.Height = height);
        SetIfChanged(transform.X, backdropTopLeft.Value.X - panelTopLeft.Value.X, x => transform.X = x);
        SetIfChanged(transform.Y, backdropTopLeft.Value.Y - panelTopLeft.Value.Y, y => transform.Y = y);
    }

    private static void SetIfChanged(double currentValue, double nextValue, Action<double> apply)
    {
        if (Math.Abs(currentValue - nextValue) < SyncTolerance)
        {
            return;
        }

        apply(nextValue);
    }

    private static Bitmap? LoadBitmap(string assetBaseName)
    {
        foreach (var extension in BackgroundExtensions)
        {
            var assetUri = new Uri($"avares://Kokkoro/Assets/Images/Auth/{assetBaseName}.{extension}");
            if (!AssetLoader.Exists(assetUri))
            {
                continue;
            }

            using var stream = AssetLoader.Open(assetUri);
            return new Bitmap(stream);
        }

        return null;
    }

    private static bool RequiresFrostSync(string? propertyName)
    {
        return propertyName is nameof(AuthWindowViewModel.PanelWidth)
            or nameof(AuthWindowViewModel.PanelHeight)
            or nameof(AuthWindowViewModel.SignInBackdropOpacity)
            or nameof(AuthWindowViewModel.SignUpBackdropOpacity);
    }

    private void AttachGeometryHandlers()
    {
        SizeChanged += OnGeometryChanged;
        AuthPanel.SizeChanged += OnGeometryChanged;
        SignInBackgroundImage.SizeChanged += OnGeometryChanged;
        SignUpBackgroundImage.SizeChanged += OnGeometryChanged;
    }

    private void DetachGeometryHandlers()
    {
        SizeChanged -= OnGeometryChanged;
        AuthPanel.SizeChanged -= OnGeometryChanged;
        SignInBackgroundImage.SizeChanged -= OnGeometryChanged;
        SignUpBackgroundImage.SizeChanged -= OnGeometryChanged;
    }

    private void AttachViewModelHandlers()
    {
        var viewModel = ViewModel;
        if (viewModel is null)
        {
            return;
        }

        viewModel.SignInSucceeded += OnSignInSucceeded;
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void DetachViewModelHandlers()
    {
        var viewModel = ViewModel;
        if (viewModel is null)
        {
            return;
        }

        viewModel.SignInSucceeded -= OnSignInSucceeded;
        viewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

}
