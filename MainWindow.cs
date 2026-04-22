using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

namespace MadeyeWsdlCSharp;

public sealed class MainWindow : Window
{
    private static readonly Color BackgroundTop = Color.FromRgb(15, 23, 42);
    private static readonly Color BackgroundBottom = Color.FromRgb(30, 41, 59);
    private static readonly Color CardFill = Color.FromRgb(248, 250, 252);
    private static readonly Color CardStroke = Color.FromRgb(226, 232, 240);
    private static readonly Color DarkPanel = Color.FromRgb(8, 15, 32);

    private readonly VisionA64Client _client = new();
    private readonly Button _systemCheckButton = new();
    private readonly Button _systemCheckExtraButton = new();
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
        Title = "VisionA64 C# Console";
        Width = 1180;
        Height = 760;
        MinWidth = 980;
        MinHeight = 680;
        Background = new SolidColorBrush(BackgroundBottom);

        Content = BuildRoot();
        SetIdleState();
    }

    private Control BuildRoot()
    {
        var root = new Grid
        {
            Margin = new Thickness(24),
            RowDefinitions = new RowDefinitions("Auto,*")
        };

        root.Children.Add(BuildHeader());
        var body = BuildBody();
        Grid.SetRow(body, 1);
        root.Children.Add(body);
        return root;
    }

    private Control BuildHeader()
    {
        var header = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
            Margin = new Thickness(0, 0, 0, 24),
            Height = 84
        };

        var titleStack = new StackPanel
        {
            Spacing = 4,
            VerticalAlignment = VerticalAlignment.Center
        };

        titleStack.Children.Add(new TextBlock
        {
            Text = "VisionA64 Console",
            Foreground = Brushes.White,
            FontSize = 28,
            FontWeight = FontWeight.Bold
        });

        titleStack.Children.Add(new TextBlock
        {
            Text = "Clean SOAP controls for the VisionA64 camera",
            Foreground = new SolidColorBrush(Color.FromRgb(191, 219, 254)),
            FontSize = 14
        });

        header.Children.Add(titleStack);

        var endpoint = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(15, 118, 110)),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(14, 10),
            VerticalAlignment = VerticalAlignment.Center,
            Child = new TextBlock
            {
                Text = "Device endpoint: http://192.168.18.244:8080/",
                Foreground = Brushes.White,
                FontSize = 13
            }
        };
        Grid.SetColumn(endpoint, 1);
        header.Children.Add(endpoint);

        return header;
    }

    private Control BuildBody()
    {
        var body = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("320,*")
        };

        body.Children.Add(BuildActionsCard());

        var resultsCard = BuildResultsCard();
        Grid.SetColumn(resultsCard, 1);
        body.Children.Add(resultsCard);
        return body;
    }

    private Control BuildActionsCard()
    {
        var card = new CardBorder(DarkPanel, CardStroke)
        {
            Padding = new Thickness(22),
            Margin = new Thickness(0, 0, 24, 0)
        };

        var stack = new StackPanel { Spacing = 14 };
        stack.Children.Add(new TextBlock
        {
            Text = "Actions",
            Foreground = Brushes.White,
            FontSize = 22,
            FontWeight = FontWeight.Bold
        });

        stack.Children.Add(new TextBlock
        {
            Text = "Run a quick health check or the extended diagnostic report from the camera.",
            Foreground = new SolidColorBrush(Color.FromRgb(203, 213, 225)),
            TextWrapping = TextWrapping.Wrap,
            Width = 260
        });

        _systemCheckButton.Content = "System Check";
        StyleButton(_systemCheckButton, Color.FromRgb(14, 165, 233));
        _systemCheckButton.Click += async (_, _) => await RunAsync("System Check", () => _client.SystemCheckAsync(1));

        _systemCheckExtraButton.Content = "System Check Extra";
        StyleButton(_systemCheckExtraButton, Color.FromRgb(168, 85, 247));
        _systemCheckExtraButton.Click += async (_, _) => await RunAsync("System Check Extra", () => _client.SystemCheckExtraAsync(1));

        _progressBar.IsVisible = false;
        _progressBar.IsIndeterminate = true;

        _statusLabel.Foreground = new SolidColorBrush(Color.FromRgb(191, 219, 254));
        _statusLabel.FontSize = 13;

        stack.Children.Add(_systemCheckButton);
        stack.Children.Add(_systemCheckExtraButton);
        stack.Children.Add(_progressBar);
        stack.Children.Add(_statusLabel);
        card.Child = stack;
        return card;
    }

    private Control BuildResultsCard()
    {
        var card = new CardBorder(CardFill, CardStroke)
        {
            Padding = new Thickness(22)
        };

        var main = new Grid
        {
            RowDefinitions = new RowDefinitions("Auto,Auto,240,*")
        };

        var header = new StackPanel { Spacing = 6 };
        header.Children.Add(new TextBlock
        {
            Text = "Latest Response",
            Foreground = new SolidColorBrush(Color.FromRgb(15, 23, 42)),
            FontSize = 24,
            FontWeight = FontWeight.Bold
        });

        header.Children.Add(_operationLabel);
        _operationLabel.Foreground = new SolidColorBrush(Color.FromRgb(71, 85, 105));
        _operationLabel.FontSize = 14;

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

        var headerRow = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,Auto"),
            Margin = new Thickness(0, 0, 0, 18)
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

        card.Child = main;
        return card;
    }

    private static Control BuildSummaryBox(string title, TextBlock valueLabel, bool wrap)
    {
        var box = new CardBorder(Colors.White, CardStroke)
        {
            Padding = new Thickness(16),
            Margin = new Thickness(0, 0, 12, 0)
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

    private static void StyleButton(Button button, Color color)
    {
        button.Height = 54;
        button.FontSize = 14;
        button.FontWeight = FontWeight.SemiBold;
        button.Background = new SolidColorBrush(color);
        button.Foreground = Brushes.White;
        button.BorderThickness = new Thickness(0);
        button.HorizontalAlignment = HorizontalAlignment.Stretch;
        button.Margin = new Thickness(0, 0, 0, 0);
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
    }

    private void ShowError(string operation, Exception ex)
    {
        _operationLabel.Text = operation;
        _resultLabel.Text = "N/A";
        _errorLabel.Text = ex.Message;
        SetStatusChip("FAILED", Color.FromRgb(239, 68, 68));

        PopulateDetails(new Dictionary<string, string>());
        PopulateReport(new[] { "The request could not be completed." });
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
        var row = new CardBorder(Color.FromRgb(248, 250, 252), CardStroke)
        {
            Padding = new Thickness(14, 12),
            Margin = new Thickness(0, 0, 0, 0)
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
        _systemCheckButton.IsEnabled = !busy;
        _systemCheckExtraButton.IsEnabled = !busy;
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
        _errorLabel.Text = "";
        _reportBox.Text = "Run one of the checks to see the camera response here.";
    }

    private void SetStatusChip(string text, Color background)
    {
        _statusChip.Background = new SolidColorBrush(background);
        if (_statusChip.Child is TextBlock child)
        {
            child.Text = text;
        }
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
