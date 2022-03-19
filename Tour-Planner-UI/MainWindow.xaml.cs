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
        //file = new() { Name = "das ist 1 test" };
        //edit = new() { Name = "das ist auch 1 test" };
        //FileButton.DataContext = file;
        //EditButton.DataContext = edit;
        //file = new() { Name = "abc", Name2 = "bcd" };

        //_MainWindowViewModel = new();
        //FileButton.DataContext = _MainWindowViewModel;
        //EditButton.DataContext = _MainWindowViewModel;
        //TourList.Tour1.DataContext = _MainWindowViewModel;
        //FileButton.DataContext = DataContext;
    }
    //private Tour[]? _AllTours;
    //private MainWindowViewModel _MainWindowViewModel;
    //private MainWindowViewModel edit;
    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        //_AllTours = RequestHandler.GetAllTours();
    }

    private void FileButtonClick(object sender, RoutedEventArgs e)
    {
        /*file.Name = "hi"*/;
    }
}
