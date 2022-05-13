namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourLogsViewModel : INotifyPropertyChanged, IObserver
{
    public TourLogsViewModel()
    {
        
    }
    private TourLog[]? AllLogs;
    public TourLog[]? Logs
    {
        get { return AllLogs; }
        set { AllLogs = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void Update(ISubject subject)
    {
        if (subject is TourDetailsViewModel model)
        {
            if (model.Tour is not null)
            {
                Logs = TourLogRepository.GetAllTourLogs(model.Tour.Id);
            }
        }
    }
}
