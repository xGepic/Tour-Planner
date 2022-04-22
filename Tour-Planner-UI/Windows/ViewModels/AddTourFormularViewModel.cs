namespace Tour_Planner_UI.Windows.ViewModels;
internal class AddTourFormularViewModel : INotifyPropertyChanged
{
    public AddTourFormularViewModel(AddTourFormular tourFormular)
    {
        AddTourFormular = tourFormular;
        SubmitTourButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public ICommand SubmitTourButtonCommand { get; set; }
    public AddTourFormular AddTourFormular{ get; set; }
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
    private ComboBoxItem TourTourType;
    public ComboBoxItem TourTourTypeInput
    {
        get { return TourTourType; }
        set { TourTourType = value; OnPropertyChanged(); }
    }
    private ComboBoxItem TourTransportType;
    public ComboBoxItem TourTransportTypeInput
    {
        get { return TourTransportType; }
        set { TourTransportType = value; OnPropertyChanged(); }
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
        string StringTourType = TourTourType.Content.ToString();
        Enum.TryParse(StringTourType, out TourType EnumTourType);
        string StringTransportType = TourTransportType.Content.ToString();
        Enum.TryParse(StringTransportType, out TransportType EnumTransportType);
        bool success = TourRepository.AddTour(TourNameInput, TourDescriptionInput, TourStartingPointInput, TourDestinationInput, EnumTransportType, EnumTourType);
        AddTourFormular.Close();
    }

}
