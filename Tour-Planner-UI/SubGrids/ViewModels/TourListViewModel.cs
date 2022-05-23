namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourListViewModel : INotifyPropertyChanged, ISubject, IObserver
{
    public TourListViewModel()
    {
        AllTours = TourRepository.GetAllTours();
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
        MinusButtonCommand = new Command(ExecuteMinusButton, CanExecuteMinusButton);
        ExportButtonCommand = new Command(ExecuteExportButton, CanExecuteExportButton);
        ImportButtonCommand = new Command(ExecuteImportButton, CanExecuteImportButton);
        Observers = new List<IObserver>();

        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
    }
    private readonly List<IObserver> Observers;
    private bool Notifing;
    public bool IsFiltered;
    public ICommand PlusButtonCommand { get; set; }
    public ICommand MinusButtonCommand { get; set; }
    public ICommand ExportButtonCommand { get; set; }
    public ICommand ImportButtonCommand { get; set; }
    private System.Windows.Media.Brush BackgroundColor;
    public System.Windows.Media.Brush Background
    {
        get { return BackgroundColor; }
        set { BackgroundColor = value; OnPropertyChanged(); }
    }
    private System.Windows.Media.Brush ForegroundColor;
    public System.Windows.Media.Brush Foreground
    {
        get { return ForegroundColor; }
        set { ForegroundColor = value; OnPropertyChanged(); }
    }
    private Tour[]? AllTours;
    public Tour[]? Tours
    {
        get { return AllTours; }
        set { AllTours = value; OnPropertyChanged(); }
    }
    private Tour? SelectedItem;
    public Tour? Selected
    {
        get { return SelectedItem; }
        set { SelectedItem = value; OnPropertyChanged(); Notify(); }
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
        TourFormular TourFormularWindow = new();
        TourFormularWindow.DataContext = new TourFormularViewModel(TourFormularWindow, string.Empty, false, Background, Foreground);
        TourFormularWindow.ShowDialog();
        Tour[]? tours = TourRepository.GetAllTours();
        if (Tours?.Length == tours?.Length || Tours is null)
        {
            Selected = null;
        }
        else
        {
            Tours = tours;
            Selected = Tours?.Last();
        }   
    }
    private bool CanExecuteMinusButton(object? parameter)
    {
        return true;
    } 
    private void ExecuteMinusButton(object? parameter)
    {
        if(Selected is not null)
        {
            TourRepository.DeleteTour(Selected.Id);
            Tours = TourRepository.GetAllTours();
            Selected = null;
        }
        else
        {
            MessageBox.Show("You have to select a tour first!");
        }
    }
    private bool CanExecuteExportButton(object? parameter)
    {
        return true;
    }
    private void ExecuteExportButton(object? parameter)
    {
        
    }
    private bool CanExecuteImportButton(object? parameter)
    {
        return true;
    }
    private void ExecuteImportButton(object? parameter)
    {
        
    }
    public void Attach(IObserver observer)
    {
        Observers.Add(observer);
    }
    public void Notify()
    {
        if (Notifing)
        {
            return;
        }
        Notifing = true;
        try
        {
            Observers.ForEach(func =>
            {
                func.Update(this);
            });
        }
        finally
        {
            Notifing = false;
        }
    }
    public void Update(ISubject subject)
    {
        if (subject is MainWindowViewModel mainModel)
        {
            Background = mainModel.Background;
            Foreground = mainModel.Foreground;
        }
        else if(subject is TourDetailsViewModel detailsModel)
        {
            if (!IsFiltered)
            {
                Tours = TourRepository.GetAllTours();
                if (detailsModel.Tour is not null)
                {
                    for (int counter = 0; counter < AllTours?.Length; counter++)
                    {
                        if (AllTours[counter].Id == detailsModel.Tour.Id)
                        {
                            Selected = detailsModel.Tour;
                            counter = AllTours.Length;
                        }
                    }
                }
            }
        } 
    }
}
