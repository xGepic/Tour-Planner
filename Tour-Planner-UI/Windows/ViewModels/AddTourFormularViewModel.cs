namespace Tour_Planner_UI.Windows.ViewModels;
internal class AddTourFormularViewModel : INotifyPropertyChanged
{
    public AddTourFormularViewModel()
    {
        SubmitTourButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public ICommand SubmitTourButtonCommand { get; set; }
    private string TourName = string.Empty;
    public string TourNameInput
    {
        get { return TourName; }
        set { TourName = value; OnPropertyChanged(); }
    }
    private string TourDescription = string.Empty;
    public string TourDescriptionInput
    {
        get { return TourDescription; }
        set { TourDescription = value; OnPropertyChanged(); }
    }
    private string TourStartingPoint = string.Empty;
    public string TourStartingPointInput
    {
        get { return TourStartingPoint; }
        set { TourStartingPoint = value; OnPropertyChanged(); }
    }
    private string TourDestination = string.Empty;
    public string TourDestinationInput
    {
        get { return TourDestination; }
        set { TourDestination = value; OnPropertyChanged(); }
    }
    private string TourTransportType;
    public string TourTransportTypeInput
    {
        get { return TourTransportType; }
        set { TourTransportType = value; OnPropertyChanged(); }
    }
    private string TourTourType;
    public string TourTourTypeInput
    {
        get { return TourTourType; }
        set { TourTourType = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private bool CanExecuteSubmitTourButton(object parameter)
    {
        return true;
    }

    private void ExecuteSubmitTourButton(object parameter)
    {
        bool success = TourRepository.AddTour(TourNameInput, TourDescriptionInput, TourStartingPointInput, TourDestinationInput, TransportType.byBus, Tourtype.Biking);
    }
}
