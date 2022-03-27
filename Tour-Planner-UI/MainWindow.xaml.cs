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
            AllTours = tours,
            tourListViewModel = new() { AllToursQ = tours}
        };
        this.DataContext = mainWindowViewModel;
    }
}
