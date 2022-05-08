namespace Tour_Planner_UI;
internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        ListViewModel = new();
        DetailsViewModel = new();
        LogsViewModel = new() { LogsListViewModel = new() };

        ListViewModel.Attach(DetailsViewModel);
        DetailsViewModel.Attach(ListViewModel);
    }
    
    public TourListViewModel? ListViewModel { get; set; }
    public TourDetailsViewModel? DetailsViewModel { get; set; }
    public TourLogsViewModel? LogsViewModel { get; set; }
    
}

