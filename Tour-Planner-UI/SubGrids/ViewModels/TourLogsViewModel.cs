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
    private Tour? RelatedTour;
    public Tour? Tour
    {
        get { return RelatedTour; }
        set { RelatedTour = value; OnPropertyChanged(); }
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
        if(Tour is not null)
        {
            TourLogFormular TourLogFormularWindow = new();
            TourLogFormularWindow.DataContext = new TourLogFormularViewModel(TourLogFormularWindow, Tour.Id);
            TourLogFormularWindow.ShowDialog();
            Logs = TourLogRepository.GetAllTourLogs(Tour.Id);
            if (Logs is null)
            {
                Selected = null;
            }
            else
            {
                Selected = Logs.Last();
            }
        }
        else
        {
            MessageBox.Show("You have to select a tour first!");
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
            Logs = TourLogRepository.GetAllTourLogs(Tour.Id);
        }
        else
        {
            MessageBox.Show("You have to select a tourlog first!");
        }
    }

    public void Update(ISubject subject)
    {
        if (subject is TourDetailsViewModel model)
        {
            if (model.Tour is not null)
            {
                Logs = TourLogRepository.GetAllTourLogs(model.Tour.Id);
                Tour = model.Tour;
            }
        }
    }
}
