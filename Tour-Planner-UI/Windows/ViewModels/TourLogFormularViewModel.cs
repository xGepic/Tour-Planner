namespace Tour_Planner_UI.Windows.ViewModels;

internal class TourLogFormularViewModel : INotifyPropertyChanged
{
    public TourLogFormularViewModel(TourLogFormular window, string id, bool isModify)
    {
        Window = window;
        IsModify = isModify;
        Id = id;
        SubmitTourButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public ICommand SubmitTourButtonCommand { get; set; }
    public TourLogFormular Window { get; set; }
    public bool IsModify { get; set; }
    private string Id;
    public string TourLogId
    {
        get { return Id; }
        set { Id = value; OnPropertyChanged(); }
    }
    private DateTime DateAndTime;
    public DateTime TourLogDateAndTime
    {
        get { return DateAndTime; }
        set { DateAndTime = value; OnPropertyChanged(); }
    }
    private string Comment = string.Empty;
    public string TourLogComment
    {
        get { return Comment; }
        set { Comment = value; OnPropertyChanged(); }
    }
    private string TimeInMin = string.Empty;
    public string TourLogTimeInMin
    {
        get { return TimeInMin; }
        set { TimeInMin = value; OnPropertyChanged(); }
    }
    private string RelatedTourId = string.Empty;
    public string TourLogRelatedTourId
    {
        get { return RelatedTourId; }
        set { RelatedTourId = value; OnPropertyChanged(); }
    }
    private ComboBoxItem Difficulty;
    public ComboBoxItem TourLogDifficulty
    {
        get { return Difficulty; }
        set { Difficulty = value; OnPropertyChanged(); }
    }
    private ComboBoxItem Rating;
    public ComboBoxItem TourLogRating
    {
        get { return Rating; }
        set { Rating = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private bool CanExecuteSubmitTourButton(object parameter)
    {
        return true;
    }

    private void ExecuteSubmitTourButton(object parameter)
    {
        if (Difficulty is not null && Rating is not null && Comment is not null && TimeInMin is not null && RelatedTourId is not null)
        {
            string DifficultyAsString = Difficulty.Content.ToString();
            _ = Enum.TryParse(DifficultyAsString, out TourDifficulty EnumDifficulty);
            string RatingAsString = Rating.Content.ToString();
            _ = Enum.TryParse(RatingAsString, out TourRating EnumRating);
            bool success;
            if (IsModify)
            {
                success = TourLogRepository.UpdateTourLog(Guid.Parse(TourLogId), TourLogDateAndTime, TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, Guid.Parse(TourLogRelatedTourId));
            }
            else
            {
                success = TourLogRepository.AddTourLog(TourLogDateAndTime, TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, Guid.Parse(TourLogRelatedTourId));
            }
            if (success)
            {
                Window.Close();
            }
            else
            {
                MessageBox.Show("Unexpected Error!");
            } 
        }
        else
        {
            MessageBox.Show("Please, fill out all the Fields!");
        }
    }
}
