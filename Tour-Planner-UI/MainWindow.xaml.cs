namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Tour[]? tours = TourRepository.GetAllTours();
        MainWindowViewModel mainWindowViewModel = new()
        {
            tourListViewModel = new() { AllTours = tours},
            tourDetailsViewModel = new(),
            tourLogsViewModel = new() { tourLogsListViewModel = new()},
        };
        this.DataContext = mainWindowViewModel;
    }
}
