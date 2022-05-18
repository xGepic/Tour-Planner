﻿namespace Tour_Planner_UI.SubGrids.ViewModels;
internal class TourLogsViewModel : INotifyPropertyChanged, IObserver
{
    public TourLogsViewModel()
    {
        PlusButtonCommand = new Command(ExecutePlusButton, CanExecutePlusButton);
        MinusButtonCommand = new Command(ExecuteMinusButton, CanExecuteMinusButton);
        ModifyButtonCommand = new Command(ExecuteModifyButton, CanExecuteModifyButton);
    }
    public ICommand PlusButtonCommand { get; set; }
    public ICommand MinusButtonCommand { get; set; }
    public ICommand ModifyButtonCommand { get; set; }
    public Tour? RelatedTour { get; set; }
    public Tour? Tour
    {
        get { return RelatedTour; }
        set { RelatedTour = value; OnPropertyChanged(); }
    }
    private TourLog[]? AllLogs;
    public TourLog[]? Logs
    {
        get { return AllLogs; }
        set { AllLogs = value; OnPropertyChanged(); }
    }
    private TourLog? SelectedItem;
    public TourLog? Selected
    {
        get { return SelectedItem; }
        set { SelectedItem = value; OnPropertyChanged();}
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
        if(Tour is not null)
        {
            TourLogFormular TourLogFormularWindow = new();
            TourLogFormularWindow.DataContext = new TourLogFormularViewModel(TourLogFormularWindow, Tour.Id);
            TourLogFormularWindow.ShowDialog();
            Logs = TourLogRepository.GetAllTourLogs(Tour.Id);
            if (Logs is null)
            {
                Selected = null;
            }
            else
            {
                Selected = Logs.Last();
            }
        }
        else
        {
            MessageBox.Show("You have to select a tour first!");
        }
    }
    private bool CanExecuteMinusButton(object? parameter)
    {
        return true;
    }
    private void ExecuteMinusButton(object? parameter)
    {
        if (Selected is not null)
        {
            TourLogRepository.DeleteTourLog(Selected.Id);
            if(Tour is not null)
            {
                Logs = TourLogRepository.GetAllTourLogs(Tour.Id);
            }
            else
            {
                MessageBox.Show("You have to select a tour first!");
            }
        }
        else
        {
            MessageBox.Show("You have to select a tourlog first!");
        }
    }
    private bool CanExecuteModifyButton(object? parameter)
    {
        if (Selected == null)
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
        TourLogFormular TourLogFormularWindow = new();
        if (Selected is not null && Tour is not null)
        {
            TourLogFormularWindow.DataContext = new TourLogFormularViewModel(TourLogFormularWindow, Tour.Id, Selected.Id)
            {
                TourLogDateAndTime = Selected.TourDateAndTime.ToString(),
                TourLogComment = Selected.TourComment,
                //TourLogDifficulty = Selected.TourDifficulty,
                TourLogTimeInMin = Selected.TourTimeInMin.ToString(),
                //TourLogRating = Selected.TourRating
            };
            TourLogFormularWindow.ShowDialog();
            Logs = TourLogRepository.GetAllTourLogs(Tour.Id);
        }
    }

    public void Update(ISubject subject)
    {
        if (subject is TourDetailsViewModel model)
        {
            if (model.Tour is not null)
            {
                Logs = TourLogRepository.GetAllTourLogs(model.Tour.Id);
                Tour = model.Tour;
            }
        }
    }
}
