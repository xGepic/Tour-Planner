namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourDetailsViewModel : INotifyPropertyChanged, IObserver, ISubject
{
    public TourDetailsViewModel()
    {
        ModifyButtonCommand = new Command(ExecuteModifyButton, CanExecuteModifyButton);
        Observers = new List<IObserver>();
    }
    private readonly List<IObserver> Observers;
    private bool Notifing;
    public ICommand ModifyButtonCommand { get; set; }
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
            TourFormularWindow.DataContext = new TourFormularViewModel(TourFormularWindow, Tour.Id.ToString(), true)
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
    public void Update(ISubject subject)
    {
        if (subject is TourListViewModel model)
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
