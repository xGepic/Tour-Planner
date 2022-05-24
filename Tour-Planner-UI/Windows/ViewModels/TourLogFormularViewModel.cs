namespace Tour_Planner_UI.Windows.ViewModels;
internal class TourLogFormularViewModel : INotifyPropertyChanged
{
    public TourLogFormularViewModel(TourLogFormular window, Guid relatedId, System.Windows.Media.Brush background, System.Windows.Media.Brush foreground)
    {
        Window = window;
        IsModify = false;
        RelatedTourId = relatedId;

        SubmitTourLogButtonCommand = new Command(ExecuteSubmitTourLogButton!, CanExecuteSubmitTourLogButton);

        Background = background;
        Foreground = foreground;
    }
    public TourLogFormularViewModel(TourLogFormular window, Guid relatedId, Guid id, System.Windows.Media.Brush background, System.Windows.Media.Brush foreground)
    {
        Window = window;
        IsModify = true;
        RelatedTourId = relatedId;
        Id = id;

        SubmitTourLogButtonCommand = new Command(ExecuteSubmitTourLogButton!, CanExecuteSubmitTourLogButton);

        Background = background;
        Foreground = foreground;
    }
    public TourLogFormular Window { get; set; }
    public bool IsModify { get; set; }
    public ICommand SubmitTourLogButtonCommand { get; set; }
    private System.Windows.Media.Brush? BackgroundColor;
    public System.Windows.Media.Brush? Background
    {
        get { return BackgroundColor; }
        set { BackgroundColor = value; OnPropertyChanged(); }
    }
    private System.Windows.Media.Brush? ForegroundColor;
    public System.Windows.Media.Brush? Foreground
    {
        get { return ForegroundColor; }
        set { ForegroundColor = value; OnPropertyChanged(); }
    }
    private Guid Id;
    public Guid TourLogId
    {
        get { return Id; }
        set { Id = value; OnPropertyChanged(); }
    }
    private string? DateAndTime;
    public string? TourLogDateAndTime
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
    private ComboBoxItem? Difficulty;
    public ComboBoxItem? TourLogDifficulty
    {
        get { return Difficulty; }
        set { Difficulty = value; OnPropertyChanged(); }
    }
    private ComboBoxItem? Rating;
    public ComboBoxItem? TourLogRating
    {
        get { return Rating; }
        set { Rating = value; OnPropertyChanged(); }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    private bool CanExecuteSubmitTourLogButton(object parameter)
    {
        return true;
    }
    private void ExecuteSubmitTourLogButton(object parameter)
    {
        if (Difficulty is not null && Rating is not null && Comment is not null && TimeInMin is not null)
        {
            if (OnlyNumbers(TimeInMin))
            {
                string? DifficultyAsString = Difficulty.Content.ToString();
                _ = Enum.TryParse(DifficultyAsString, out TourDifficulty EnumDifficulty);
                string? RatingAsString = Rating.Content.ToString();
                _ = Enum.TryParse(RatingAsString, out TourRating EnumRating);
                bool success;
                if (IsModify)
                {
                    success = TourLogRepository.UpdateTourLog(TourLogId, DateTime.Parse(TourLogDateAndTime!), TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, TourLogRelatedTourId);
                }
                else
                {
                    success = TourLogRepository.AddTourLog(DateTime.Parse(TourLogDateAndTime!), TourLogComment, EnumDifficulty, uint.Parse(TourLogTimeInMin), EnumRating, TourLogRelatedTourId);
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
    private static bool OnlyNumbers(string input)
    {
        Regex regex = new("[0-9]+");
        return regex.IsMatch(input);
    }
}
