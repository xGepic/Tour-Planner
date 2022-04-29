namespace Tour_Planner_UI;
internal class MainWindowViewModel
{
    public MainWindowViewModel()
    {
        tourListViewModel = new();
        tourDetailsViewModel = new();
        tourLogsViewModel = new() { tourLogsListViewModel = new() };
    }
    public Tour[]? tours { get; set; }
    public TourListViewModel? tourListViewModel { get; set; }
    public TourDetailsViewModel? tourDetailsViewModel { get; set; }
    public TourLogsViewModel? tourLogsViewModel { get; set; }
}

