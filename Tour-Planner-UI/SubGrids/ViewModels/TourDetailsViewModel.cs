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
    public ImageSource? MapImage {
        get { return Image; }
        set { Image = value; OnPropertyChanged(); }
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
                    MapImage = MapRepository.CallStaticmapUri(Tour.StartingPoint, Tour.Destination);
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
