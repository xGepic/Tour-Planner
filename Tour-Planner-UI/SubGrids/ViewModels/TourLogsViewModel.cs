namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourLogsViewModel : INotifyPropertyChanged, IObserver
{
    public TourLogsViewModel()
    {
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
        MinusButtonCommand = new Command(ExecuteMinusButton, CanExecuteMinusButton);
    }
    public ICommand PlusButtonCommand { get; set; }
    public ICommand MinusButtonCommand { get; set; }
    private Guid RelatedTourId;
    public Guid TourId
    {
        get { return RelatedTourId; }
        set { RelatedTourId = value; OnPropertyChanged(); }
    }
    private TourLog[]? AllLogs;
    public TourLog[]? Logs
    {
        get { return AllLogs; }
        set { AllLogs = value; OnPropertyChanged(); }
    }
    private TourLog? SelectedItem;
    public TourLog? Selected
    {
        get { return SelectedItem; }
        set { SelectedItem = value; OnPropertyChanged();}
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private bool CanExecutePlusButton(object? parameter)
    {
        return true;
    }

    private void ExecutePlusButton(object? parameter)
    {
        TourLogFormular TourLogFormularWindow = new();
        TourLogFormularWindow.DataContext = new TourLogFormularViewModel(TourLogFormularWindow, string.Empty, false);
        TourLogFormularWindow.ShowDialog();
        Logs = TourLogRepository.GetAllTourLogs(TourId);
        if (Logs is null)
        {
            Selected = null;
        }
        else
        {
            Selected = Logs.Last();
        }
    }
    private bool CanExecuteMinusButton(object? parameter)
    {
        return true;
    }
    private void ExecuteMinusButton(object? parameter)
    {
        if (Selected is not null)
        {
            TourLogRepository.DeleteTourLog(Selected.Id);
            Logs = TourLogRepository.GetAllTourLogs(TourId);
        }
        else
        {
            MessageBox.Show("You have to select a tour first!");
        }
    }

    public void Update(ISubject subject)
    {
        if (subject is TourDetailsViewModel model)
        {
            if (model.Tour is not null)
            {
                Logs = TourLogRepository.GetAllTourLogs(model.Tour.Id);
                TourId = model.Tour.Id;
            }
        }
    }
}
