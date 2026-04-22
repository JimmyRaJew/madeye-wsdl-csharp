using System.Drawing.Drawing2D;

namespace MadeyeWsdlCSharp;

internal sealed class MainForm : Form
{
    private static readonly Color BackgroundTop = Color.FromArgb(15, 23, 42);
    private static readonly Color BackgroundBottom = Color.FromArgb(30, 41, 59);
    private static readonly Color CardFill = Color.FromArgb(248, 250, 252);
    private static readonly Color CardBorder = Color.FromArgb(226, 232, 240);
    private static readonly Color DarkPanel = Color.FromArgb(8, 15, 32);

    private readonly VisionA64Client _client = new();
    private readonly Button _systemCheckButton = new();
    private readonly Button _systemCheckExtraButton = new();
    private readonly Label _operationLabel = new();
    private readonly Label _resultLabel = new();
    private readonly Label _errorLabel = new();
    private readonly Label _statusChip = new();
    private readonly FlowLayoutPanel _detailsBody = new();
    private readonly TextBox _reportBox = new();
    private readonly ProgressBar _progressBar = new();
    private readonly Label _statusLabel = new();

    public MainForm()
    {
        Text = "VisionA64 C# Console";
        MinimumSize = new Size(1180, 760);
        StartPosition = FormStartPosition.CenterScreen;
        DoubleBuffered = true;
        BackColor = BackgroundBottom;

        BuildUi();
        SetIdleState();
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        using var brush = new LinearGradientBrush(ClientRectangle, BackgroundTop, BackgroundBottom, LinearGradientMode.Vertical);
        e.Graphics.FillRectangle(brush, ClientRectangle);
    }

    private void BuildUi()
    {
        var root = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24),
            BackColor = Color.Transparent
        };

        var header = BuildHeader();
        var body = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.Transparent,
            Padding = new Padding(0, 24, 0, 0)
        };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        body.Controls.Add(BuildActionsCard(), 0, 0);
        body.Controls.Add(BuildResultsCard(), 1, 0);

        root.Controls.Add(body);
        root.Controls.Add(header);
        Controls.Add(root);
    }

    private Control BuildHeader()
    {
        var header = new Panel
        {
            Dock = DockStyle.Top,
            Height = 84,
            BackColor = Color.Transparent
        };

        var title = new Label
        {
            Text = "VisionA64 Console",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 26, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(0, 0)
        };

        var subtitle = new Label
        {
            Text = "Clean SOAP controls for the VisionA64 camera",
            ForeColor = Color.FromArgb(191, 219, 254),
            Font = new Font("Segoe UI", 11, FontStyle.Regular),
            AutoSize = true,
            Location = new Point(2, 42)
        };

        var endpoint = new Label
        {
            Text = "Device endpoint: http://192.168.18.244:8080/",
            AutoSize = true,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(15, 118, 110),
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            Padding = new Padding(14, 10, 14, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
        };
        endpoint.Location = new Point(Width - endpoint.Width - 120, 0);

        header.Controls.Add(title);
        header.Controls.Add(subtitle);
        header.Controls.Add(endpoint);
        return header;
    }

    private Control BuildActionsCard()
    {
        var card = new CardPanel(DarkPanel, CardBorder)
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(22)
        };

        var title = new Label
        {
            Text = "Actions",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            AutoSize = true
        };

        var help = new Label
        {
            Text = "Run a quick health check or the extended diagnostic report from the camera.",
            ForeColor = Color.FromArgb(203, 213, 225),
            Font = new Font("Segoe UI", 10.5f),
            AutoSize = false,
            Width = 260,
            Height = 60
        };

        _systemCheckButton.Text = "System Check";
        _systemCheckButton.BackColor = Color.FromArgb(14, 165, 233);
        _systemCheckButton.ForeColor = Color.White;
        _systemCheckButton.FlatStyle = FlatStyle.Flat;
        _systemCheckButton.FlatAppearance.BorderSize = 0;
        _systemCheckButton.Width = 260;
        _systemCheckButton.Height = 54;
        _systemCheckButton.Click += async (_, _) => await RunAsync("System Check", () => _client.SystemCheckAsync(1));

        _systemCheckExtraButton.Text = "System Check Extra";
        _systemCheckExtraButton.BackColor = Color.FromArgb(168, 85, 247);
        _systemCheckExtraButton.ForeColor = Color.White;
        _systemCheckExtraButton.FlatStyle = FlatStyle.Flat;
        _systemCheckExtraButton.FlatAppearance.BorderSize = 0;
        _systemCheckExtraButton.Width = 260;
        _systemCheckExtraButton.Height = 54;
        _systemCheckExtraButton.Click += async (_, _) => await RunAsync("System Check Extra", () => _client.SystemCheckExtraAsync(1));

        _progressBar.Style = ProgressBarStyle.Marquee;
        _progressBar.MarqueeAnimationSpeed = 30;
        _progressBar.Visible = false;
        _progressBar.Width = 260;

        _statusLabel.Text = "Idle";
        _statusLabel.ForeColor = Color.FromArgb(191, 219, 254);
        _statusLabel.Font = new Font("Segoe UI", 10.5f);
        _statusLabel.AutoSize = true;

        var stack = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = false
        };

        stack.Controls.Add(title);
        stack.Controls.Add(help);
        stack.Controls.Add(new Panel { Height = 16, Width = 260 });
        stack.Controls.Add(_systemCheckButton);
        stack.Controls.Add(new Panel { Height = 10, Width = 260 });
        stack.Controls.Add(_systemCheckExtraButton);
        stack.Controls.Add(new Panel { Height = 16, Width = 260 });
        stack.Controls.Add(_progressBar);
        stack.Controls.Add(new Panel { Height = 14, Width = 260 });
        stack.Controls.Add(_statusLabel);

        card.Controls.Add(stack);
        return card;
    }

    private Control BuildResultsCard()
    {
        var card = new CardPanel(CardFill, CardBorder)
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(22)
        };

        var title = new Label
        {
            Text = "Latest Response",
            ForeColor = Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            AutoSize = true
        };

        _operationLabel.Text = "Idle";
        _operationLabel.ForeColor = Color.FromArgb(71, 85, 105);
        _operationLabel.Font = new Font("Segoe UI", 10.5f);
        _operationLabel.AutoSize = true;

        _statusChip.Text = "READY";
        _statusChip.AutoSize = true;
        _statusChip.ForeColor = Color.White;
        _statusChip.BackColor = Color.FromArgb(100, 116, 139);
        _statusChip.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        _statusChip.Padding = new Padding(10, 7, 10, 7);

        var header = new Panel { Dock = DockStyle.Top, Height = 72, BackColor = Color.Transparent };
        header.Controls.Add(_statusChip);
        header.Controls.Add(title);
        header.Controls.Add(_operationLabel);
        _statusChip.Location = new Point(header.Width - 120, 4);
        title.Location = new Point(0, 0);
        _operationLabel.Location = new Point(2, 34);
        _statusChip.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        var summary = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 100,
            ColumnCount = 2,
            BackColor = Color.Transparent
        };
        summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        summary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));

        summary.Controls.Add(BuildSummaryBox("Result Code", _resultLabel), 0, 0);
        summary.Controls.Add(BuildSummaryBox("Error Message", _errorLabel, true), 1, 0);

        var detailsCard = new CardPanel(Color.White, CardBorder)
        {
            Dock = DockStyle.Top,
            Height = 240,
            Padding = new Padding(16)
        };
        var detailsTitle = new Label
        {
            Text = "Details",
            ForeColor = Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            AutoSize = true
        };

        _detailsBody.Dock = DockStyle.Fill;
        _detailsBody.AutoScroll = true;
        _detailsBody.FlowDirection = FlowDirection.TopDown;
        _detailsBody.WrapContents = false;

        detailsCard.Controls.Add(_detailsBody);
        detailsCard.Controls.Add(detailsTitle);
        detailsTitle.Dock = DockStyle.Top;

        var reportCard = new CardPanel(Color.White, CardBorder)
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16)
        };
        var reportTitle = new Label
        {
            Text = "Report",
            ForeColor = Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            AutoSize = true
        };

        _reportBox.Dock = DockStyle.Fill;
        _reportBox.Multiline = true;
        _reportBox.ReadOnly = true;
        _reportBox.ScrollBars = ScrollBars.Vertical;
        _reportBox.BorderStyle = BorderStyle.None;
        _reportBox.BackColor = Color.White;
        _reportBox.ForeColor = Color.FromArgb(30, 41, 59);
        _reportBox.Font = new Font("Consolas", 10);

        reportCard.Controls.Add(_reportBox);
        reportCard.Controls.Add(reportTitle);
        reportTitle.Dock = DockStyle.Top;

        var body = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1,
            BackColor = Color.Transparent
        };
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 72));
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
        body.RowStyles.Add(new RowStyle(SizeType.Absolute, 240));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        body.Controls.Add(header, 0, 0);
        body.Controls.Add(summary, 0, 1);
        body.Controls.Add(detailsCard, 0, 2);
        body.Controls.Add(reportCard, 0, 3);

        card.Controls.Add(body);
        return card;
    }

    private static Control BuildSummaryBox(string label, Label valueLabel, bool wrap = false)
    {
        var box = new CardPanel(Color.White, CardBorder)
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(16)
        };

        var title = new Label
        {
            Text = label,
            ForeColor = Color.FromArgb(71, 85, 105),
            Font = new Font("Segoe UI", 9.5f),
            AutoSize = true,
            Dock = DockStyle.Top
        };

        valueLabel.Dock = DockStyle.Fill;
        valueLabel.Font = new Font("Segoe UI", wrap ? 11 : 24, wrap ? FontStyle.Regular : FontStyle.Bold);
        valueLabel.ForeColor = wrap ? Color.FromArgb(185, 28, 28) : Color.FromArgb(15, 23, 42);
        valueLabel.AutoSize = false;
        valueLabel.TextAlign = ContentAlignment.MiddleLeft;
        valueLabel.Height = wrap ? 48 : 50;

        box.Controls.Add(valueLabel);
        box.Controls.Add(title);
        return box;
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
        SetStatusChip(result.Result == 0 ? "OK" : "ERROR", result.Result == 0 ? Color.FromArgb(16, 185, 129) : Color.FromArgb(239, 68, 68));

        PopulateDetails(result.Details);
        PopulateReport(result.ReportLines);
    }

    private void ShowError(string operation, Exception ex)
    {
        _operationLabel.Text = operation;
        _resultLabel.Text = "N/A";
        _errorLabel.Text = ex.Message;
        SetStatusChip("FAILED", Color.FromArgb(239, 68, 68));

        PopulateDetails(new Dictionary<string, string>());
        PopulateReport(new[] { "The request could not be completed." });
    }

    private void PopulateDetails(IReadOnlyDictionary<string, string> details)
    {
        _detailsBody.SuspendLayout();
        _detailsBody.Controls.Clear();

        if (details.Count == 0)
        {
            _detailsBody.Controls.Add(new Label
            {
                Text = "No additional details returned.",
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true
            });
        }
        else
        {
            foreach ((string key, string value) in details)
            {
                _detailsBody.Controls.Add(BuildDetailRow(key, value));
            }
        }

        _detailsBody.ResumeLayout();
    }

    private void PopulateReport(IReadOnlyList<string> lines)
    {
        _reportBox.Text = lines.Count == 0
            ? "Report is empty."
            : string.Join(Environment.NewLine, lines);
        _reportBox.SelectionStart = 0;
        _reportBox.SelectionLength = 0;
    }

    private static Control BuildDetailRow(string key, string value)
    {
        var row = new CardPanel(Color.FromArgb(248, 250, 252), CardBorder)
        {
            Width = 520,
            Height = 48,
            Margin = new Padding(0, 0, 0, 10),
            Padding = new Padding(12, 10, 12, 10)
        };

        var keyLabel = new Label
        {
            Text = key,
            ForeColor = Color.FromArgb(51, 65, 85),
            Font = new Font("Segoe UI", 9.5f),
            AutoSize = true,
            Dock = DockStyle.Left
        };

        var valueLabel = new Label
        {
            Text = string.IsNullOrWhiteSpace(value) ? "(empty)" : value,
            ForeColor = Color.FromArgb(15, 23, 42),
            Font = new Font("Segoe UI", 10.5f, FontStyle.Regular),
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleRight
        };

        row.Controls.Add(valueLabel);
        row.Controls.Add(keyLabel);
        return row;
    }

    private void SetBusy(bool busy, string status)
    {
        _progressBar.Visible = busy;
        _systemCheckButton.Enabled = !busy;
        _systemCheckExtraButton.Enabled = !busy;
        _statusLabel.Text = status;

        if (busy)
        {
            SetStatusChip("WORKING", Color.FromArgb(59, 130, 246));
        }
    }

    private void SetIdleState()
    {
        _statusLabel.Text = "Idle";
        SetStatusChip("READY", Color.FromArgb(100, 116, 139));
        _resultLabel.Text = "No data yet";
        _errorLabel.Text = "";
        _reportBox.Text = "Run one of the checks to see the camera response here.";
    }

    private void SetStatusChip(string text, Color background)
    {
        _statusChip.Text = text;
        _statusChip.BackColor = background;
    }

    private sealed class CardPanel : Panel
    {
        private readonly Color _fill;
        private readonly Color _border;

        public CardPanel(Color fill, Color border)
        {
            _fill = fill;
            _border = border;
            DoubleBuffered = true;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var fillBrush = new SolidBrush(_fill);
            using var borderPen = new Pen(_border);
            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using var path = CreateRoundedRect(rect, 24);
            e.Graphics.FillPath(fillBrush, path);
            e.Graphics.DrawPath(borderPen, path);
            base.OnPaint(e);
        }

        private static GraphicsPath CreateRoundedRect(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}

