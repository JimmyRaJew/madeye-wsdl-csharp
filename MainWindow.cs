using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform.Storage;

namespace MadeyeWsdlCSharp;

public sealed class MainWindow : Window
{
    private static readonly Color CardFill = Color.FromRgb(255, 255, 255);
    private static readonly Color CardStroke = Color.FromRgb(226, 232, 240);
    private static readonly Color SoftFill = Color.FromRgb(248, 250, 252);

    private readonly VisionA64Client _client = new();
    private readonly List<Button> _actionButtons = new();

    private readonly Button _menuButton = new();
    private readonly Border _menuPanel = new();
    private readonly TextBlock _operationLabel = new();
    private readonly TextBlock _resultLabel = new();
    private readonly TextBlock _errorLabel = new();
    private readonly Border _statusChip = new();
    private readonly StackPanel _detailsBody = new();
    private readonly TextBox _reportBox = new();
    private readonly ProgressBar _progressBar = new();
    private readonly TextBlock _statusLabel = new();

    public MainWindow()
    {
        Title = "VisionA64 Console";
        Width = 1180;
        Height = 760;
        MinWidth = 980;
        MinHeight = 680;
        Background = Brushes.White;

        Content = BuildRoot();
        SetIdleState();
    }

    private Control BuildRoot()
    {
        var root = new Grid
        {
            Margin = new Thickness(16),
            RowDefinitions = new RowDefinitions("Auto,*"),
            Background = Brushes.White
        };

        root.Children.Add(BuildTopBar());

        var body = BuildBody();
        Grid.SetRow(body, 1);
        root.Children.Add(body);

        ConfigureMenuPanel();
        Grid.SetRowSpan(_menuPanel, 2);
        root.Children.Add(_menuPanel);

        return root;
    }

    private Control BuildTopBar()
    {
        var header = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto"),
            Margin = new Thickness(0, 0, 0, 12),
            VerticalAlignment = VerticalAlignment.Center
        };

        _menuButton.Content = "☰";
        _menuButton.Width = 44;
        _menuButton.Height = 36;
        _menuButton.FontSize = 18;
        _menuButton.FontWeight = FontWeight.Bold;
        _menuButton.Background = Brushes.White;
        _menuButton.BorderBrush = new SolidColorBrush(CardStroke);
        _menuButton.BorderThickness = new Thickness(1);
        _menuButton.Click += (_, _) => ToggleMenu();

        header.Children.Add(_menuButton);

        var titleStack = new StackPanel
        {
            Spacing = 2,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(12, 0, 0, 0)
        };

        titleStack.Children.Add(new TextBlock
        {
            Text = "VisionA64 Console",
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 28,
            FontWeight = FontWeight.Bold
        });

        titleStack.Children.Add(new TextBlock
        {
            Text = "Clean SOAP controls for the VisionA64 camera",
            Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105)),
            FontSize = 14
        });

        Grid.SetColumn(titleStack, 1);
        header.Children.Add(titleStack);

        var endpoint = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(240, 249, 255)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(191, 219, 254)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(14, 10),
            VerticalAlignment = VerticalAlignment.Center,
            Child = new TextBlock
            {
                Text = "Device endpoint 192.168.18.244:8080",
                Foreground = new SolidColorBrush(Color.FromRgb(30, 64, 175)),
                FontSize = 13
            }
        };
        Grid.SetColumn(endpoint, 2);
        header.Children.Add(endpoint);

        return header;
    }

    private Control BuildBody()
    {
        var body = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*")
        };

        var resultsCard = BuildResultsCard();
        body.Children.Add(resultsCard);
        return body;
    }

    private Control BuildResultsCard()
    {
        var card = new CardBorder(CardFill, CardStroke)
        {
            Padding = new Thickness(22)
        };

        var main = new Grid
        {
            RowDefinitions = new RowDefinitions("Auto,Auto,Auto,*")
        };

        var headerRow = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
            Margin = new Thickness(0, 0, 0, 18)
        };

        var header = new StackPanel { Spacing = 6 };
        header.Children.Add(new TextBlock
        {
            Text = "Latest Response",
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 24,
            FontWeight = FontWeight.Bold
        });

        _operationLabel.Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105));
        _operationLabel.FontSize = 14;
        header.Children.Add(_operationLabel);

        _statusChip.Background = new SolidColorBrush(Color.FromRgb(100, 116, 139));
        _statusChip.CornerRadius = new CornerRadius(999);
        _statusChip.Padding = new Thickness(14, 8);
        _statusChip.HorizontalAlignment = HorizontalAlignment.Right;
        _statusChip.Child = new TextBlock
        {
            Text = "READY",
            Foreground = Brushes.White,
            FontWeight = FontWeight.Bold
        };

        headerRow.Children.Add(header);
        Grid.SetColumn(_statusChip, 1);
        headerRow.Children.Add(_statusChip);
        main.Children.Add(headerRow);

        var summary = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("3*,7*"),
            Margin = new Thickness(0, 0, 0, 14)
        };
        var resultBox = BuildSummaryBox("Result Code", _resultLabel, false);
        var errorBox = BuildSummaryBox("Error Message", _errorLabel, true);
        Grid.SetColumn(errorBox, 1);
        summary.Children.Add(resultBox);
        summary.Children.Add(errorBox);
        main.Children.Add(summary);
        Grid.SetRow(summary, 1);

        var detailsCard = new CardBorder(Colors.White, CardStroke)
        {
            Padding = new Thickness(16),
            Margin = new Thickness(0, 0, 0, 14)
        };
        var detailsStack = new StackPanel { Spacing = 10 };
        detailsStack.Children.Add(new TextBlock
        {
            Text = "Details",
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 18,
            FontWeight = FontWeight.Bold
        });
        _detailsBody.Spacing = 10;
        detailsStack.Children.Add(_detailsBody);
        detailsCard.Child = detailsStack;
        main.Children.Add(detailsCard);
        Grid.SetRow(detailsCard, 2);

        var reportCard = new CardBorder(Colors.White, CardStroke)
        {
            Padding = new Thickness(16)
        };
        var reportStack = new StackPanel { Spacing = 10 };
        reportStack.Children.Add(new TextBlock
        {
            Text = "Report",
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 18,
            FontWeight = FontWeight.Bold
        });

        _reportBox.IsReadOnly = true;
        _reportBox.TextWrapping = TextWrapping.Wrap;
        _reportBox.AcceptsReturn = true;
        _reportBox.Background = Brushes.Transparent;
        _reportBox.BorderThickness = new Thickness(0);
        _reportBox.FontFamily = new FontFamily("Consolas");
        _reportBox.FontSize = 14;
        _reportBox.Height = 220;
        reportStack.Children.Add(_reportBox);
        reportCard.Child = reportStack;
        main.Children.Add(reportCard);
        Grid.SetRow(reportCard, 3);

        var footer = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Margin = new Thickness(0, 10, 0, 0)
        };

        _progressBar.IsVisible = false;
        _progressBar.IsIndeterminate = true;
        _progressBar.Width = 220;
        _progressBar.Margin = new Thickness(0, 0, 12, 0);

        _statusLabel.Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139));
        _statusLabel.FontSize = 12;
        _statusLabel.VerticalAlignment = VerticalAlignment.Center;

        footer.Children.Add(_progressBar);
        footer.Children.Add(_statusLabel);
        Grid.SetRow(footer, 4);
        main.RowDefinitions = new RowDefinitions("Auto,Auto,Auto,*,Auto");
        main.Children.Add(footer);

        card.Child = main;
        return card;
    }

    private static Control BuildSummaryBox(string title, TextBlock valueLabel, bool wrap)
    {
        var box = new CardBorder(Colors.White, CardStroke)
        {
            Padding = new Thickness(16)
        };

        var stack = new StackPanel { Spacing = 6 };
        stack.Children.Add(new TextBlock
        {
            Text = title,
            Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105)),
            FontSize = 10.5
        });

        valueLabel.FontSize = wrap ? 12 : 28;
        valueLabel.FontWeight = wrap ? FontWeight.Normal : FontWeight.Bold;
        valueLabel.Foreground = wrap ? new SolidColorBrush(Color.FromRgb(185, 28, 28)) : new SolidColorBrush(Color.FromRgb(15, 23, 42));
        valueLabel.TextWrapping = wrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
        stack.Children.Add(valueLabel);
        box.Child = stack;
        return box;
    }

    private void ConfigureMenuPanel()
    {
        _menuPanel.Width = 280;
        _menuPanel.IsVisible = false;
        _menuPanel.Background = Brushes.White;
        _menuPanel.BorderBrush = new SolidColorBrush(CardStroke);
        _menuPanel.BorderThickness = new Thickness(1);
        _menuPanel.CornerRadius = new CornerRadius(18);
        _menuPanel.HorizontalAlignment = HorizontalAlignment.Left;
        _menuPanel.VerticalAlignment = VerticalAlignment.Top;
        _menuPanel.Margin = new Thickness(0, 52, 0, 0);
        _menuPanel.BoxShadow = new BoxShadows(new BoxShadow
        {
            Color = Color.FromArgb(28, 15, 23, 42),
            Blur = 18,
            OffsetX = 0,
            OffsetY = 8
        });

        var panel = new StackPanel
        {
            Spacing = 8,
            Margin = new Thickness(12)
        };

        panel.Children.Add(BuildMenuSection("Quick Checks",
            BuildMenuButton("System Check", async () => await RunAsync("System Check", () => _client.SystemCheckAsync(1))),
            BuildMenuButton("System Check Extra", async () => await RunAsync("System Check Extra", () => _client.SystemCheckExtraAsync(1)))));

        panel.Children.Add(BuildMenuSection("System Settings",
            BuildMenuButton("Device ID Get", async () => await RunAsync("System Device ID Get", () => _client.SystemDeviceIdGetAsync(1))),
            BuildMenuButton("Description Get", async () => await RunAsync("System Description Get", () => _client.SystemDescriptionGetAsync(1))),
            BuildMenuButton("Description Set", PromptAndRunDescriptionSet)));

        panel.Children.Add(BuildMenuSection("Maintenance",
            BuildMenuButton("System Restart", ConfirmAndRunRestart),
            BuildMenuButton("Firmware Update", PromptAndRunFirmwareUpdate)));

        _menuPanel.Child = panel;
    }

    private Control BuildMenuSection(string title, params Button[] buttons)
    {
        var stack = new StackPanel { Spacing = 6 };

        stack.Children.Add(new TextBlock
        {
            Text = title,
            Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139)),
            FontSize = 12,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(6, 4, 6, 2)
        });

        foreach (var button in buttons)
        {
            stack.Children.Add(button);
        }

        return stack;
    }

    private Button BuildMenuButton(string title, Func<Task> action)
    {
        var button = new Button
        {
            Content = title,
            HorizontalContentAlignment = HorizontalAlignment.Left,
            Background = Brushes.White,
            BorderBrush = new SolidColorBrush(CardStroke),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Margin = new Thickness(0, 0, 0, 0),
            Padding = new Thickness(12, 10),
            FontSize = 14
        };

        button.Click += async (_, _) =>
        {
            _menuPanel.IsVisible = false;
            await action();
        };

        _actionButtons.Add(button);
        return button;
    }

    private void ToggleMenu()
    {
        _menuPanel.IsVisible = !_menuPanel.IsVisible;
    }

    private async Task ConfirmAndRunRestart()
    {
        bool confirmed = await ShowConfirmDialogAsync(
            "Confirm System Restart",
            "System Restart will reboot the camera after it responds. Continue?");

        if (!confirmed)
        {
            return;
        }

        await RunAsync("System Restart", () => _client.SystemRestartAsync(1));
    }

    private async Task PromptAndRunFirmwareUpdate()
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select firmware ZIP file",
            AllowMultiple = false
        });

        if (files.Count == 0)
        {
            return;
        }

        string? md5 = await ShowTextPromptAsync(
            "Firmware MD5",
            "Enter the first 32 characters of the MD5 sum:");

        if (string.IsNullOrWhiteSpace(md5))
        {
            return;
        }

        try
        {
            using var stream = await files[0].OpenReadAsync();
            using var memory = new MemoryStream();
            await stream.CopyToAsync(memory);
            await RunAsync("System Firmware Update", () => _client.SystemFirmwareUpdateAsync(memory.ToArray(), md5.Trim()));
        }
        catch (Exception ex)
        {
            ShowError("System Firmware Update", ex);
        }
    }

    private async Task PromptAndRunDescriptionSet()
    {
        var labels = await ShowTriplePromptAsync(
            "Set System Description",
            "Label 1",
            "Label 2",
            "Label 3");

        if (labels is null)
        {
            return;
        }

        await RunAsync(
            "System Description Set",
            () => _client.SystemDescriptionSetAsync(labels.Value.Label1, labels.Value.Label2, labels.Value.Label3));
    }

    private async Task RunAsync(string operation, Func<Task<SoapResult>> action)
    {
        SetBusy(true, $"{operation} in progress...");

        try
        {
            SoapResult result = await action();
            ShowResult(result);
        }
        catch (Exception ex)
        {
            ShowError(operation, ex);
        }
        finally
        {
            SetBusy(false, "Idle");
        }
    }

    private void ShowResult(SoapResult result)
    {
        _operationLabel.Text = result.Operation;
        _resultLabel.Text = result.Result.ToString();
        _errorLabel.Text = string.IsNullOrWhiteSpace(result.ErrorMessage) ? "(empty)" : result.ErrorMessage;
        SetStatusChip(result.Result == 0 ? "OK" : "ERROR", result.Result == 0 ? Color.FromRgb(16, 185, 129) : Color.FromRgb(239, 68, 68));

        PopulateDetails(result.Details);
        PopulateReport(result.ReportLines);
        _statusLabel.Text = $"Last run: {result.Operation}";
    }

    private void ShowError(string operation, Exception ex)
    {
        _operationLabel.Text = operation;
        _resultLabel.Text = "N/A";
        _errorLabel.Text = ex.Message;
        SetStatusChip("FAILED", Color.FromRgb(239, 68, 68));

        PopulateDetails(new Dictionary<string, string>());
        PopulateReport(new[] { "The request could not be completed." });
        _statusLabel.Text = "Last run failed";
    }

    private void PopulateDetails(IReadOnlyDictionary<string, string> details)
    {
        _detailsBody.Children.Clear();

        if (details.Count == 0)
        {
            _detailsBody.Children.Add(new TextBlock
            {
                Text = "No additional details returned.",
                Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105))
            });
            return;
        }

        foreach ((string key, string value) in details)
        {
            _detailsBody.Children.Add(BuildDetailRow(key, value));
        }
    }

    private void PopulateReport(IReadOnlyList<string> lines)
    {
        _reportBox.Text = lines.Count == 0
            ? "Report is empty."
            : string.Join(Environment.NewLine, lines);
    }

    private static Control BuildDetailRow(string key, string value)
    {
        var row = new CardBorder(SoftFill, CardStroke)
        {
            Padding = new Thickness(14, 12)
        };

        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*")
        };

        grid.Children.Add(new TextBlock
        {
            Text = key,
            Foreground = new SolidColorBrush(Color.FromRgb(51, 65, 85)),
            FontSize = 13,
            VerticalAlignment = VerticalAlignment.Center
        });

        grid.Children.Add(new TextBlock
        {
            Text = string.IsNullOrWhiteSpace(value) ? "(empty)" : value,
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 14,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center,
            TextAlignment = TextAlignment.Right
        });

        Grid.SetColumn(grid.Children[1], 1);
        row.Child = grid;
        return row;
    }

    private void SetBusy(bool busy, string status)
    {
        _progressBar.IsVisible = busy;
        _menuButton.IsEnabled = !busy;
        foreach (var button in _actionButtons)
        {
            button.IsEnabled = !busy;
        }
        _statusLabel.Text = status;

        if (busy)
        {
            SetStatusChip("WORKING", Color.FromRgb(59, 130, 246));
        }
    }

    private void SetIdleState()
    {
        _statusLabel.Text = "Idle";
        SetStatusChip("READY", Color.FromRgb(100, 116, 139));
        _resultLabel.Text = "No data yet";
        _errorLabel.Text = "No request has run yet";
        _reportBox.Text = "Use the burger menu at the top-left to start.";
    }

    private void SetStatusChip(string text, Color background)
    {
        _statusChip.Background = new SolidColorBrush(background);
        if (_statusChip.Child is TextBlock child)
        {
            child.Text = text;
        }
    }

    private async Task<bool> ShowConfirmDialogAsync(string title, string message)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 440,
            Height = 180,
            CanResize = false,
            Background = Brushes.White,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        bool confirmed = false;
        dialog.Content = BuildDialogShell(
            message,
            new[]
            {
                BuildDialogButton("Cancel", () => dialog.Close()),
                BuildDialogButton("Continue", () =>
                {
                    confirmed = true;
                    dialog.Close();
                })
            });

        await dialog.ShowDialog(this);
        return confirmed;
    }

    private async Task<string?> ShowTextPromptAsync(string title, string message)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 440,
            Height = 190,
            CanResize = false,
            Background = Brushes.White,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        var input = new TextBox { MinWidth = 320 };
        string? value = null;

        dialog.Content = BuildDialogShell(
            message,
            new Control[]
            {
                input,
                BuildDialogButton("Cancel", () => dialog.Close()),
                BuildDialogButton("OK", () =>
                {
                    value = input.Text;
                    dialog.Close();
                })
            });

        await dialog.ShowDialog(this);
        return value;
    }

    private async Task<(string Label1, string Label2, string Label3)?> ShowTriplePromptAsync(
        string title,
        string label1,
        string label2,
        string label3)
    {
        var dialog = new Window
        {
            Title = title,
            Width = 500,
            Height = 300,
            CanResize = false,
            Background = Brushes.White,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        var input1 = new TextBox();
        var input2 = new TextBox();
        var input3 = new TextBox();
        (string Label1, string Label2, string Label3)? value = null;

        var form = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*"),
            RowDefinitions = new RowDefinitions("Auto,Auto,Auto,Auto"),
            RowSpacing = 10,
            ColumnSpacing = 12,
            Margin = new Thickness(20)
        };

        AddLabeledInput(form, label1, input1, 0);
        AddLabeledInput(form, label2, input2, 1);
        AddLabeledInput(form, label3, input3, 2);

        var actions = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Spacing = 10
        };

        actions.Children.Add(BuildDialogButton("Cancel", () => dialog.Close()));
        actions.Children.Add(BuildDialogButton("OK", () =>
        {
            value = (input1.Text?.Trim() ?? string.Empty, input2.Text?.Trim() ?? string.Empty, input3.Text?.Trim() ?? string.Empty);
            dialog.Close();
        }));

        Grid.SetRow(actions, 3);
        Grid.SetColumnSpan(actions, 2);
        form.Children.Add(actions);

        dialog.Content = form;
        await dialog.ShowDialog(this);
        return value;
    }

    private Control BuildDialogShell(string message, IEnumerable<Control> controls)
    {
        var stack = new StackPanel
        {
            Margin = new Thickness(20),
            Spacing = 16
        };

        stack.Children.Add(new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap,
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42))
        });

        foreach (var control in controls)
        {
            stack.Children.Add(control);
        }

        return stack;
    }

    private static void AddLabeledInput(Grid grid, string label, TextBox input, int row)
    {
        var text = new TextBlock
        {
            Text = label,
            Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105)),
            VerticalAlignment = VerticalAlignment.Center
        };
        grid.Children.Add(text);
        Grid.SetRow(text, row);

        input.MinWidth = 300;
        grid.Children.Add(input);
        Grid.SetRow(input, row);
        Grid.SetColumn(input, 1);
    }

    private Button BuildDialogButton(string title, Action action)
    {
        var button = new Button
        {
            Content = title,
            Padding = new Thickness(14, 8),
            Background = Brushes.White,
            BorderBrush = new SolidColorBrush(CardStroke),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10)
        };

        button.Click += (_, _) => action();
        return button;
    }

    private sealed class CardBorder : Border
    {
        public CardBorder(Color fill, Color stroke)
        {
            Background = new SolidColorBrush(fill);
            BorderBrush = new SolidColorBrush(stroke);
            BorderThickness = new Thickness(1);
            CornerRadius = new CornerRadius(24);
        }
    }

}
