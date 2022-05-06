namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourDetailsViewModel : INotifyPropertyChanged, IObserver
{
    private Tour? SelectedTour;
    public Tour? Tour
    {
        get { return SelectedTour; }
        set { SelectedTour = value; OnPropertyChanged(); }
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
}
