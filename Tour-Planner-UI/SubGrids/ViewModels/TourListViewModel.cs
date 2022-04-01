namespace Tour_Planner_UI.SubGrids.ViewModels;

internal class TourListViewModel
{
    public Tour[]? AllTours { get; set; }
    public ICommand command { get; set; }
    public TourListViewModel(Tour[]? tours)
    {
        AllTours = tours;
        command = new Command(ExecutePlusButton, CanExecutePlusButton);
    }

    private bool CanExecutePlusButton(object parameter)
    {
        return true;
    }

    private void ExecutePlusButton(object parameter)
    {
        AddTourFormularWindow addTourFormularWindow = new();
        addTourFormularWindow.DataContext = this;
        addTourFormularWindow.ShowDialog();
    }
}
