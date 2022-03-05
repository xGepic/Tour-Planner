namespace Tour_Planner_Model;
public class TourLog
{
    public DateTime TourDateAndTime { get; set; }
    public string? TourComment { get; set; }
    public TourDifficulty TourDifficulty { get; set; }
    public uint TourTimeInMin { get; set; }
    public TourRating TourRating { get; set; }
}