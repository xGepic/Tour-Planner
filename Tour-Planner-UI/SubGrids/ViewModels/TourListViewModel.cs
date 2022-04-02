namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourListViewModel : INotifyPropertyChanged
{
    
    public ICommand PlusButtonCommand { get; set; }
    public TourListViewModel(Tour[]? tours)
    {
        AllTours = tours;
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
    }
    public Tour[]? AllTours { get; set; }
    public Tour[] Tours
    {
        get { return AllTours; }
        set { AllTours = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private bool CanExecutePlusButton(object parameter)
    {
        return true;
    }

    private void ExecutePlusButton(object parameter)
    {
        AddTourFormular addTourFormular = new();
        addTourFormular.DataContext = new AddTourFormularViewModel();
        addTourFormular.ShowDialog();
    }
    
}
