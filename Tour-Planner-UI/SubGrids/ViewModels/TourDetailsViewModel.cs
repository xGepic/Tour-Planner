namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourDetailsViewModel : INotifyPropertyChanged, IObserver, ISubject
{
    public TourDetailsViewModel()
    {
        ModifyButtonCommand = new Command(ExecuteModifyButton, CanExecuteModifyButton);
        TourReportButtonCommand = new Command(ExecuteTourReportButton, CanExecuteTourReportButton);

        Observers = new List<IObserver>();

        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
    }
    private readonly List<IObserver> Observers;
    private bool Notifing;
    public ICommand ModifyButtonCommand { get; set; }
    public ICommand TourReportButtonCommand { get; set; }
    
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
    private Tour? SelectedTour;
    public Tour? Tour
    {
        get { return SelectedTour; }
        set { SelectedTour = value; OnPropertyChanged(); Notify(); }
    }
    private ImageSource? Image;
    public ImageSource? MapImage
    {
        get { return Image; }
        set { Image = value; OnPropertyChanged(); }
    }
    private byte[]? ImageByteArray;
    public byte[]? MapImageByteArray
    {
        get { return ImageByteArray; }
        set { ImageByteArray = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private bool CanExecuteModifyButton(object? parameter)
    {
        if(Tour == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ExecuteModifyButton(object? parameter)
    {
        TourFormular TourFormularWindow = new();
        if(Tour is not null)
        {
            TourFormularWindow.DataContext = new TourFormularViewModel(TourFormularWindow, Tour.Id.ToString(), true, Background, Foreground)
            {
                TourName = Tour.TourName,
                TourDescription = Tour.TourDescription,
                TourStartingPoint = Tour.StartingPoint,
                TourDestination = Tour.Destination,
                //TourTransportType = new ComboBoxItem() { Content = Tour.TransportType.ToString() },
                //TourType = new ComboBoxItem() { Content = Tour.TourType.ToString() },
            };
            TourFormularWindow.ShowDialog();
            Tour = TourRepository.GetTourById(Tour.Id);
        }
    }
    private bool CanExecuteTourReportButton(object? parameter)
    {
        return true;
    }

    private void ExecuteTourReportButton(object? parameter)
    {
        if(Tour is null)
        {
            MessageBox.Show("Please select a Tour first!");
        }
        else
        {
            bool success = ReportRepository.TourReport(Tour);
            if (success)
            {
                MessageBox.Show("TourReport has been generated!");
            }
            else
            {
                MessageBox.Show("Something went wrong!");
            }
        } 
    }
    
    public void Update(ISubject subject)
    {
        if (subject is MainWindowViewModel mainModel)
        {
            Background = mainModel.Background;
            Foreground = mainModel.Foreground;
        }
        else if (subject is TourListViewModel model)
        {
            Tour = model.Selected;
            if(Tour is not null)
            {
                if(Tour.StartingPoint is not null && Tour.Destination is not null)
                {
                    MapImageByteArray = MapRepository.CallStaticmapUri(Tour.StartingPoint, Tour.Destination);
                    if(MapImageByteArray is not null)
                    {
                        Bitmap? bitmap = (Bitmap?)new ImageConverter().ConvertFrom(MapImageByteArray);
                        if(bitmap is not null)
                        {
                            MapImage = BitmapToBitmapImage.ConvertToBitmapImage(bitmap);
                        }
                        else
                        {
                            MapImage = null;
                        }
                    }
                    else
                    {
                        MapImage = null;
                    }
                }
                else
                {
                    MapImage = null;
                }
            }
            else
            {
                Tour = null;
                MapImage = null;
            }
        }
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
    
}
