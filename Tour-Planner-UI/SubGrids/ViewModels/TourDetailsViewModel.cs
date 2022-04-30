namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourDetailsViewModel : INotifyPropertyChanged, IObserver
{
    private Tour? Tour;
    public Tour? tour
    {
        get { return Tour; }
        set { Tour = value; OnPropertyChanged(); }
    }
    private ImageSource MapImage;
    public ImageSource mapImage {
        get { return MapImage; }
        set { MapImage = value; OnPropertyChanged(); }
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
            tour = model.Selected;
            if(tour.StartingPoint == null || tour.Destination == null)
            {
                MessageBox.Show("Unexpected Error");
            }
            else
            {
                mapImage = MapRepository.CallStaticmapUri(tour.StartingPoint, tour.Destination);
            }
        }
    }
}
