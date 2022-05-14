namespace Tour_Planner_UI.Windows.ViewModels;
internal class TourLogFormularViewModel : INotifyPropertyChanged
{
    public TourLogFormularViewModel(TourLogFormular window, Guid relatedId)
    {
        Window = window;
        IsModify = false;
        RelatedTourId = relatedId;
        SubmitTourLogButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public TourLogFormularViewModel(TourLogFormular window, Guid relatedId, Guid id)
    {
        Window = window;
        IsModify = true;
        RelatedTourId = relatedId;
        Id = id;
        SubmitTourLogButtonCommand = new Command(ExecuteSubmitTourButton, CanExecuteSubmitTourButton);
    }
    public ICommand SubmitTourLogButtonCommand { get; set; }
    public TourLogFormular Window { get; set; }
    public bool IsModify { get; set; }
    private Guid Id;
    public Guid TourLogId
    {
        get { return Id; }
        set { Id = value; OnPropertyChanged(); }
    }
    private string DateAndTime;
    public string TourLogDateAndTime
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
    private Guid RelatedTourId;
    public Guid TourLogRelatedTourId
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

    private bool OnlyNumbers(string input)
    {
        Regex regex = new Regex("[0-9]+");
        return regex.IsMatch(input);
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
        if (Difficulty is not null && Rating is not null && Comment is not null && TimeInMin is not null)
        {
            if (OnlyNumbers(TimeInMin))
            {
                string DifficultyAsString = Difficulty.Content.ToString();
                _ = Enum.TryParse(DifficultyAsString, out TourDifficulty EnumDifficulty);
                string RatingAsString = Rating.Content.ToString();
                _ = Enum.TryParse(RatingAsString, out TourRating EnumRating);
                bool success;
                if (IsModify)
                {
                    success = TourLogRepository.UpdateTourLog(TourLogId, DateTime.Parse(TourLogDateAndTime), TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, TourLogRelatedTourId);
                }
                else
                {
                    success = TourLogRepository.AddTourLog(DateTime.Parse(TourLogDateAndTime), TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, TourLogRelatedTourId);
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
                MessageBox.Show("Time in Min has to be a number!");
            }
        }
        else
        {
            MessageBox.Show("Please, fill out all the Fields!");
        }
    }
}
