namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourLogsViewModel : IObserver
{
    public TourLogsViewModel()
    {
        //PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
        //MinusButtonCommand = new Command(ExecuteMinusButton, CanExecuteMinusButton);
        //Observers = new List<IObserver>();
    }
    //private readonly List<IObserver> Observers;
    //private bool Notifing;
    //public ICommand PlusButtonCommand { get; set; }
    //public ICommand MinusButtonCommand { get; set; }
    private TourLog[]? AllLogs;
    public TourLog[]? Logs
    {
        get { return AllLogs; }
        set { AllLogs = value; OnPropertyChanged(); }
    }
    //private TourLog? SelectedItem;
    //public TourLog? Selected
    //{
    //    get { return SelectedItem; }
    //    set { SelectedItem = value; OnPropertyChanged(); /*Notify();*/ }
    //}

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }



    //private bool CanExecutePlusButton(object? parameter)
    //{
    //    return true;
    //}

    //private void ExecutePlusButton(object? parameter)
    //{
    //    TourFormular TourFormularWindow = new();
    //    TourFormularWindow.DataContext = new TourFormularViewModel(TourFormularWindow, string.Empty, false);
    //    TourFormularWindow.ShowDialog();
    //    Tours = TourRepository.GetAllTours();
    //    if (Tours is null)
    //    {
    //        Selected = null;
    //    }
    //    else
    //    {
    //        Selected = Tours.Last();
    //    }
    //}
    //private bool CanExecuteMinusButton(object? parameter)
    //{
    //    return true;
    //}
    //private void ExecuteMinusButton(object? parameter)
    //{
    //    if (Selected is not null)
    //    {
    //        TourRepository.DeleteTour(Selected.Id);
    //        Tours = TourRepository.GetAllTours();
    //    }
    //    else
    //    {
    //        MessageBox.Show("You have to select a tour first!");
    //    }
    //}
    //public void Attach(IObserver observer)
    //{
    //    Observers.Add(observer);
    //}
    //public void Notify()
    //{
    //    if (Notifing)
    //    {
    //        return;
    //    }
    //    Notifing = true;
    //    try
    //    {
    //        Observers.ForEach(func =>
    //        {
    //            func.Update(this);
    //        });
    //    }
    //    finally
    //    {
    //        Notifing = false;
    //    }
    //}
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
