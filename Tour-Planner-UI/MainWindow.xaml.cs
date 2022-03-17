namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        TourList tourList = new();
        TourListGrid.Children.Add(tourList);
        TourDetails tourDetails = new();
        TourDetailsGrid.Children.Add(tourDetails);
        TourLogs tourLogs = new();
        TourLogsGrid.Children.Add(tourLogs);
        _AllTours = RequestHandler.GetAllTours();
    }
    private readonly Tour[]? _AllTours;

    private void FileButton_Click(object sender, RoutedEventArgs e)
    {
        
    }
}
