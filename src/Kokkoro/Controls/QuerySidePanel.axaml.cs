using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace Kokkoro.Controls;

public partial class QuerySidePanel : ContentControl
{
    private static readonly string[] EmptyHeaderCharacters = Array.Empty<string>();

    public static readonly StyledProperty<string> HeaderProperty =
        AvaloniaProperty.Register<QuerySidePanel, string>(nameof(Header), "查询条件");

    public static readonly StyledProperty<bool> IsOpenProperty =
        AvaloniaProperty.Register<QuerySidePanel, bool>(nameof(IsOpen), true);

    public static readonly StyledProperty<double> ExpandedWidthProperty =
        AvaloniaProperty.Register<QuerySidePanel, double>(nameof(ExpandedWidth), 220);

    public static readonly StyledProperty<double> CollapsedWidthProperty =
        AvaloniaProperty.Register<QuerySidePanel, double>(nameof(CollapsedWidth), 32);

    public static readonly StyledProperty<ICommand?> QueryCommandProperty =
        AvaloniaProperty.Register<QuerySidePanel, ICommand?>(nameof(QueryCommand));

    public static readonly StyledProperty<ICommand?> ResetCommandProperty =
        AvaloniaProperty.Register<QuerySidePanel, ICommand?>(nameof(ResetCommand));

    public static readonly StyledProperty<string> QueryTextProperty =
        AvaloniaProperty.Register<QuerySidePanel, string>(nameof(QueryText), "查询");

    public static readonly StyledProperty<string> ResetTextProperty =
        AvaloniaProperty.Register<QuerySidePanel, string>(nameof(ResetText), "清空");

    internal static readonly StyledProperty<double> CurrentWidthProperty =
        AvaloniaProperty.Register<QuerySidePanel, double>(nameof(CurrentWidth));

    internal static readonly StyledProperty<IReadOnlyList<string>> HeaderCharactersProperty =
        AvaloniaProperty.Register<QuerySidePanel, IReadOnlyList<string>>(
            nameof(HeaderCharacters),
            EmptyHeaderCharacters);

    public string Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    /// <summary>True: expanded docked panel; False: collapsed rail.</summary>
    public bool IsOpen
    {
        get => GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    public double ExpandedWidth
    {
        get => GetValue(ExpandedWidthProperty);
        set => SetValue(ExpandedWidthProperty, value);
    }

    public double CollapsedWidth
    {
        get => GetValue(CollapsedWidthProperty);
        set => SetValue(CollapsedWidthProperty, value);
    }

    public ICommand? QueryCommand
    {
        get => GetValue(QueryCommandProperty);
        set => SetValue(QueryCommandProperty, value);
    }

    public ICommand? ResetCommand
    {
        get => GetValue(ResetCommandProperty);
        set => SetValue(ResetCommandProperty, value);
    }

    public string QueryText
    {
        get => GetValue(QueryTextProperty);
        set => SetValue(QueryTextProperty, value);
    }

    public string ResetText
    {
        get => GetValue(ResetTextProperty);
        set => SetValue(ResetTextProperty, value);
    }

    internal double CurrentWidth
    {
        get => GetValue(CurrentWidthProperty);
        private set => SetValue(CurrentWidthProperty, value);
    }

    internal IReadOnlyList<string> HeaderCharacters
    {
        get => GetValue(HeaderCharactersProperty);
        private set => SetValue(HeaderCharactersProperty, value);
    }

    public ICommand OpenCommand { get; }
    public ICommand ToggleDockCommand { get; }

    public QuerySidePanel()
    {
        InitializeComponent();

        OpenCommand = new RelayCommand(() => IsOpen = true);
        ToggleDockCommand = new RelayCommand(() => IsOpen = false);

        UpdateHeaderCharacters(Header);
        UpdatePanelWidth();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        //if (change.Property == HeaderProperty)
        //{
        //    UpdateHeaderCharacters(Header);
        //}
        //else if (change.Property == IsOpenProperty
        //         || change.Property == ExpandedWidthProperty
        //         || change.Property == CollapsedWidthProperty)
        //{
        //    UpdatePanelWidth();
        //}
    }

    private void UpdateHeaderCharacters(string? header)
    {
        HeaderCharacters = string.IsNullOrEmpty(header)
            ? EmptyHeaderCharacters
            : header.Select(static c => c.ToString()).ToArray();
    }

    private void UpdatePanelWidth() =>
        CurrentWidth = IsOpen ? ExpandedWidth : CollapsedWidth;

    private sealed class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
    {
        public bool CanExecute(object? parameter) => canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => execute();

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
