namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourListViewModel : INotifyPropertyChanged, ISubject
{
    public TourListViewModel()
    {
        AllTours = TourRepository.GetAllTours();
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
        MinusButtonCommand = new Command(ExecuteMinusButton, CanExecuteMinusButton);
        Observers = new List<IObserver>();
    }
    private Tour[]? AllTours;
    public Tour[] Tours
    {
        get { return AllTours; }
        set { AllTours = value; OnPropertyChanged(); }
    }
    private Tour SelectedItem;
    public Tour Selected
    {
        get { return SelectedItem; }
        set { SelectedItem = value; OnPropertyChanged(); Notify(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public ICommand PlusButtonCommand { get; set; }
    public ICommand MinusButtonCommand { get; set; }

    private bool CanExecutePlusButton(object parameter)
    {
        return true;
    }

    private void ExecutePlusButton(object parameter)
    {
        AddTourFormular addTourFormular = new();
        addTourFormular.DataContext = new AddTourFormularViewModel(addTourFormular);
        addTourFormular.ShowDialog();
        Tours = TourRepository.GetAllTours();
    }
    private bool CanExecuteMinusButton(object parameter)
    {
        return true;
    } 

    private void ExecuteMinusButton(object parameter)
    {
        if(Selected is not null)
        {
            TourRepository.DeleteTour(Selected.Id);
            Tours = TourRepository.GetAllTours();
        }
        else
        {
            MessageBox.Show("You have to select a tour first!");
        }
    }

    private List<IObserver> Observers;
    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void Notify()
    {
        Observers.ForEach(func =>
        {
            func.Update(this);
        });
    }
}
