namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void FileButton_Click(object sender, RoutedEventArgs e)
    {
        using (HttpClient client = new HttpClient())
        {
            Uri endpoint = new Uri("https://localhost:7122/TourLog/GetAll");
            HttpResponseMessage Response = client.GetAsync(endpoint).Result;
            string ResultAsString = Response.Content.ReadAsStringAsync().Result;
            Tour_Planner_Model.TourLog[]? AllLogs = JsonConvert.DeserializeObject<Tour_Planner_Model.TourLog[]>(ResultAsString);
        }
    }
}
