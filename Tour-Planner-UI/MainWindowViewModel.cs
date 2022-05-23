namespace Tour_Planner_UI;
internal class MainWindowViewModel : INotifyPropertyChanged, ISubject
{
    public MainWindowViewModel()
    {
        ListViewModel = new();
        DetailsViewModel = new();
        LogsViewModel = new();

        Observers = new List<IObserver>();
        this.Attach(ListViewModel);
        this.Attach(DetailsViewModel);
        this.Attach(LogsViewModel);

        ListViewModel.Attach(DetailsViewModel);
        DetailsViewModel.Attach(ListViewModel);
        DetailsViewModel.Attach(LogsViewModel);

        SearchButtonCommand = new Command(ExecuteSearchButton, CanExecuteSearchButton);
        DarkLightButtonCommand = new Command(ExecuteDarkLightButton, CanExecuteDarkLightButton);

        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
    }
    private readonly List<IObserver> Observers;
    private bool Notifing;
    public TourListViewModel? ListViewModel { get; set; }
    public TourDetailsViewModel? DetailsViewModel { get; set; }
    public TourLogsViewModel? LogsViewModel { get; set; }
    public ICommand SearchButtonCommand { get; set; }
    public ICommand DarkLightButtonCommand { get; set; }
    private System.Windows.Media.Brush BackgroundColor;
    public System.Windows.Media.Brush Background
    {
        get{ return BackgroundColor;}
        set{ BackgroundColor = value; OnPropertyChanged(); Notify(); }
    }
    private System.Windows.Media.Brush ForegroundColor;
    public System.Windows.Media.Brush Foreground
    {
        get { return ForegroundColor; }
        set { ForegroundColor = value; OnPropertyChanged(); Notify(); }
    }
    private string SearchInput;
    public string Search
    {
        get { return SearchInput; }
        set { SearchInput = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private bool CanExecuteSearchButton(object? parameter)
    {
        return true;
    }

    private void ExecuteSearchButton(object? parameter)
    {
        if (ListViewModel is not null)
        {
            if ((bool)parameter)
            {
                ListViewModel.IsFiltered = true;
                ListViewModel.Tours = ListViewModel.Tours.Where(e =>
                    e.TourName.Contains(Search) ||
                    e.TourDescription.Contains(Search) ||
                    e.StartingPoint.Contains(Search) ||
                    e.Destination.Contains(Search) ||
                    e.TransportType.ToString().Contains(Search) ||
                    e.TourDistance.ToString().Contains(Search) ||
                    e.EstimatedTimeInMin.ToString().Contains(Search) ||
                    e.TourType.ToString().Contains(Search) ||
                    e.Popularity.ToString().Contains(Search) ||
                    e.ChildFriendliness.ToString().Contains(Search) ||
                    e.TourLogs.Any(f =>
                        f.TourDateAndTime.ToString().Contains(Search) ||
                        f.TourComment.Contains(Search) ||
                        f.TourDifficulty.ToString().Contains(Search) ||
                        f.TourTimeInMin.ToString().Contains(Search) ||
                        f.TourRating.ToString().Contains(Search)
                )).ToArray();
                
            }
            else
            {
                ListViewModel.IsFiltered = false;
                Tour temp = ListViewModel.Selected;
                ListViewModel.Tours = TourRepository.GetAllTours();
                ListViewModel.Selected = temp;
            } 
        }
        
    }
    private bool CanExecuteDarkLightButton(object? parameter)
    {
        return true;
    }

    private void ExecuteDarkLightButton(object? parameter)
    {
        if ((bool)parameter)
        {
            Background = new SolidColorBrush(Colors.Black);
            Foreground = new SolidColorBrush(Colors.White);
        }
        else
        {
            Background = new SolidColorBrush(Colors.White);
            Foreground = new SolidColorBrush(Colors.Black);
        }
    }

    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }
    public void Notify()
    {
        if (Notifing)
        {
            return;
        }
        Notifing = true;
        try
        {
            Observers.ForEach(func =>
            {
                func.Update(this);
            });
        }
        finally
        {
            Notifing = false;
        }
    }
}

