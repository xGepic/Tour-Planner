namespace Tour_Planner_UI.Windows.ViewModels;
internal class TourFormularViewModel : INotifyPropertyChanged
{
    public TourFormularViewModel(TourFormular window, string id, bool isModify)
    {
        Window = window;
        IsModify = isModify;
        Id = id;
        SubmitTourButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public ICommand SubmitTourButtonCommand { get; set; }
    public TourFormular Window { get; set; }
    public bool IsModify { get; set; }
    private string Id;
    public string TourId 
    {
        get { return Id; }
        set { Id = value; OnPropertyChanged();} 
    }
    private string Name = string.Empty;
    public string TourName
    {
        get { return Name; }
        set { Name = value; OnPropertyChanged(); }
    }
    private string Description = string.Empty;
    public string TourDescription
    {
        get { return Description; }
        set { Description = value; OnPropertyChanged(); }
    }
    private string StartingPoint = string.Empty;
    public string TourStartingPoint
    {
        get { return StartingPoint; }
        set { StartingPoint = value; OnPropertyChanged(); }
    }
    private string Destination = string.Empty;
    public string TourDestination
    {
        get { return Destination; }
        set { Destination = value; OnPropertyChanged(); }
    }
    private ComboBoxItem Type;
    public ComboBoxItem TourType
    {
        get { return Type; }
        set { Type = value; OnPropertyChanged(); }
    }
    
    private ComboBoxItem TransportType;
    public ComboBoxItem TourTransportType
    {
        get { return TransportType; }
        set { TransportType = value; OnPropertyChanged(); }
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
        if (Type is not null && TransportType is not null && Name is not null && Description is not null && StartingPoint is not null && Destination is not null)
        {
            string TypeAsString = Type.Content.ToString();
            _ = Enum.TryParse(TypeAsString, out TourType EnumTourType);
            string TransportTypeAsString = TransportType.Content.ToString();
            _ = Enum.TryParse(TransportTypeAsString, out TransportType EnumTransportType);
            Tuple<double?, uint?> DistanceAndTime = MapRepository.CallDirectionsUri(StartingPoint, Destination);
            if (DistanceAndTime.Item1 == null || DistanceAndTime.Item2 == null)
            {
                MessageBox.Show("Please, make sure that START and DESTINATION are real places!");
            }
            else
            {
                bool success = false;
                if (IsModify)
                {
                    success = TourRepository.UpdateTour(Guid.Parse(TourId), TourName, TourDescription, TourStartingPoint, TourDestination, EnumTransportType, EnumTourType, (double)DistanceAndTime.Item1, (uint)DistanceAndTime.Item2);
                }
                else
                {
                    success = TourRepository.AddTour(TourName, TourDescription, TourStartingPoint, TourDestination, EnumTransportType, EnumTourType, (double)DistanceAndTime.Item1, (uint)DistanceAndTime.Item2);
                }
                if (success)
                {
                    Window.Close();
                }
                else
                {
                    MessageBox.Show("Unexpected Error!");
                }
            }
        }
        else
        {
            MessageBox.Show("Please, fill out all the Fields!");
        }
    }

}
