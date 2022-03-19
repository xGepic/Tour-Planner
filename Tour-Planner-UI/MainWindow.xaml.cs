namespace Tour_Planner_UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new MainWindowViewModel();
        InitializeComponent();
    }
    
    
    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void FileButtonClick(object sender, RoutedEventArgs e)
    {

    }
}
