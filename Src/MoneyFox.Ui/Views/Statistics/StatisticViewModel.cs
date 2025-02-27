namespace MoneyFox.Ui.Views.Statistics;

using System.Globalization;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Core.Common.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using MediatR;
using Resources.Strings;
using SkiaSharp;

internal abstract class StatisticViewModel : BasePageViewModel, IRecipient<DateSelectedMessage>
{
    protected readonly IMediator Mediator;
    private DateTime endDate;
    private DateTime startDate;

    protected StatisticViewModel(IMediator mediator) : this(
        startDate: DateTime.Today.GetFirstDayOfMonth(),
        endDate: DateTime.Today.GetLastDayOfMonth(),
        mediator: mediator) { }

    private StatisticViewModel(DateTime startDate, DateTime endDate, IMediator mediator)
    {
        StartDate = startDate;
        EndDate = endDate;
        Mediator = mediator;

        // If Application.Current is null, application is running in the context of a unit test
        if (Application.Current is not null)
        {
            var textColor = Application.Current.RequestedTheme == AppTheme.Dark
                ? (Color)App.ResourceDictionary["Colors"]["TextPrimaryColorDark"]
                : (Color)App.ResourceDictionary["Colors"]["TextPrimaryColorLight"];

            LegendTextPaint = new() { Color = new(textColor.ToUint()), SKTypeface = SKTypeface.FromFamilyName("Courier New") };
            var legendBackgroundColor = Application.Current.RequestedTheme == AppTheme.Dark
                ? (Color)App.ResourceDictionary["Colors"]["StatisticLegendDark"]
                : (Color)App.ResourceDictionary["Colors"]["StatisticLegendLight"];

            LegendBackgroundPaint = new() { Color = new(legendBackgroundColor.ToUint()) };
        }
    }

    public SolidColorPaint LegendTextPaint { get; } = new();

    public SolidColorPaint LegendBackgroundPaint { get; } = new();

    public AsyncRelayCommand LoadedCommand => new(LoadAsync);

    public DateTime StartDate
    {
        get => startDate;

        protected set
        {
            startDate = value;
            OnPropertyChanged();

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(Title));
        }
    }

    public DateTime EndDate
    {
        get => endDate;

        private set
        {
            endDate = value;
            OnPropertyChanged();

            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Title
        => $"{Translations.StatisticsTimeRangeTitle} {StartDate.ToString(format: "d", provider: CultureInfo.InvariantCulture)} - {EndDate.ToString(format: "d", provider: CultureInfo.InvariantCulture)}";

    public void Receive(DateSelectedMessage message)
    {
        StartDate = message.StartDate;
        EndDate = message.EndDate;
        LoadAsync().GetAwaiter().GetResult();
    }

    protected abstract Task LoadAsync();
}
