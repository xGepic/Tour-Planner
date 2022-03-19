namespace Tour_Planner_UI;
internal class MainWindowViewModel : INotifyPropertyChanged, INotifyPropertyChanging
{
    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    //public string Name { get; set; }
    //public string Name { get; set; }
    //public string Name2 { get; set; }
    public Tour[]? _AllTours { get; set; } = RequestHandler.GetAllTours();

}

