namespace Tour_Planner_UI;
internal class MainWindowViewModel : INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
        ListViewModel = new();
        DetailsViewModel = new();
        LogsViewModel = new();

        ListViewModel.Attach(DetailsViewModel);
        DetailsViewModel.Attach(ListViewModel);
        DetailsViewModel.Attach(LogsViewModel);
        SearchButtonCommand = new Command(ExecuteSearchButton, CanExecuteSearchButton);
        SearchButtonCommand = new Command(ExecuteSearchButton, CanExecuteSearchButton);
    }
    
    public TourListViewModel? ListViewModel { get; set; }
    public TourDetailsViewModel? DetailsViewModel { get; set; }
    public TourLogsViewModel? LogsViewModel { get; set; }
    public ICommand SearchButtonCommand { get; set; }
    public bool IsChecked { get; set; }
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
}

