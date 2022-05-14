namespace Tour_Planner_UI;
internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        ListViewModel = new();
        DetailsViewModel = new();
        LogsViewModel = new();

        ListViewModel.Attach(DetailsViewModel);
        //ListViewModel.Attach(LogsViewModel);
        DetailsViewModel.Attach(ListViewModel);
        DetailsViewModel.Attach(LogsViewModel);
    }
    
    public TourListViewModel? ListViewModel { get; set; }
    public TourDetailsViewModel? DetailsViewModel { get; set; }
    public TourLogsViewModel? LogsViewModel { get; set; }
    
}

