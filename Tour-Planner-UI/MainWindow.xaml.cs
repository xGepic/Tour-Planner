namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UserControls.TourList tourList = new UserControls.TourList();
        TourListGrid.Children.Add(tourList);
        UserControls.TourDetails tourDetails = new UserControls.TourDetails();
        TourDetailsGrid.Children.Add(tourDetails);
        AllTourLogs = RequestHandler.GetAllTourLogs();
        UserControls.TourLogs tourLogs = new UserControls.TourLogs();
        TourLogsGrid.Children.Add(tourLogs);
    }
    private RequestHandler _requestHandler = new RequestHandler();
    private Tour_Planner_Model.TourLog[]? AllTourLogs;
    private Tour_Planner_Model.Tour[]? AllTours;

    private void FileButton_Click(object sender, RoutedEventArgs e)
    {
        
    }
}
