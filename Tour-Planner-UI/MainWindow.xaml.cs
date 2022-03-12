namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        TourList tourList = new TourList();
        TourListGrid.Children.Add(tourList);
        AllTourLogs = _requestHandler.GetAllTourLogs();
    }
    private RequestHandler _requestHandler = new RequestHandler();
    private Tour_Planner_Model.TourLog[]? AllTourLogs;
    private Tour_Planner_Model.Tour[]? AllTours;

    private void FileButton_Click(object sender, RoutedEventArgs e)
    {
        
    }
}
