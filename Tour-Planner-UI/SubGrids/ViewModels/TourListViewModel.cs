namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourListViewModel
{
    public Tour[]? AllTours { get; set; }
    public ICommand PlusButtonCommand { get; set; }
    public TourListViewModel(Tour[]? tours)
    {
        AllTours = tours;
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
    }
    private bool CanExecutePlusButton(object parameter)
    {
        return true;
    }

    private void ExecutePlusButton(object parameter)
    {
        AddTourFormular addTourFormular = new();
        addTourFormular.DataContext = new AddTourFormularViewModel();
        addTourFormular.ShowDialog();
    }
    
}
